using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VocabularyApp.Data;
using VocabularyApp.Data.Models;
using VocabularyApp.WebApi.DTOs;
using VocabularyApp.WebApi.DTOs.External;

namespace VocabularyApp.WebApi.Services;

public class WordService : IWordService
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WordService> _logger;
    private const string DictionaryApiBaseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en";

    public WordService(ApplicationDbContext context, HttpClient httpClient, ILogger<WordService> logger)
    {
        _context = context;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WordLookupResponse> LookupWordAsync(string word)
    {
        try
        {
            // Normalize the word (lowercase, trim)
            var normalizedWord = word.Trim().ToLowerInvariant();
            
            // First, check if word exists in our local database
            var existingWord = await GetWordFromCacheAsync(normalizedWord);
            if (existingWord != null)
            {
                _logger.LogInformation("Word '{Word}' found in local cache", word);
                return new WordLookupResponse
                {
                    Success = true,
                    Word = existingWord,
                    WasFoundInCache = true
                };
            }

            // Not in cache, call external API
            _logger.LogInformation("Word '{Word}' not in cache, calling external API", word);
            var apiResponse = await CallDictionaryApiAsync(normalizedWord);
            
            if (apiResponse == null)
            {
                return new WordLookupResponse
                {
                    Success = false,
                    ErrorMessage = $"Word '{word}' not found in dictionary"
                };
            }

            // Save to local database and return
            var savedWord = await SaveWordToDatabaseAsync(apiResponse);
            
            return new WordLookupResponse
            {
                Success = true,
                Word = savedWord,
                WasFoundInCache = false
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error looking up word '{Word}'", word);
            return new WordLookupResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while looking up the word"
            };
        }
    }

    public async Task<WordDto?> GetWordFromCacheAsync(string word)
    {
        var normalizedWord = word.Trim().ToLowerInvariant();
        
        var wordEntity = await _context.Words
            .Include(w => w.WordDefinitions)
            .ThenInclude(wd => wd.PartOfSpeech)
            .FirstOrDefaultAsync(w => w.Text == normalizedWord);

        if (wordEntity == null)
            return null;

        return MapWordToDto(wordEntity);
    }

    public async Task<List<WordDto>> SearchWordsAsync(string searchTerm, int maxResults = 50)
    {
        var normalizedTerm = searchTerm.Trim().ToLowerInvariant();
        
        var words = await _context.Words
            .Include(w => w.WordDefinitions)
            .ThenInclude(wd => wd.PartOfSpeech)
            .Where(w => w.Text.Contains(normalizedTerm))
            .OrderBy(w => w.Text)
            .Take(maxResults)
            .ToListAsync();

        return words.Select(MapWordToDto).ToList();
    }

    public async Task<object> GetWordStatisticsAsync()
    {
        var totalWords = await _context.Words.CountAsync();
        var totalDefinitions = await _context.WordDefinitions.CountAsync();
        var partOfSpeechStats = await _context.WordDefinitions
            .GroupBy(wd => wd.PartOfSpeech.Name)
            .Select(g => new { PartOfSpeech = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        return new
        {
            TotalWords = totalWords,
            TotalDefinitions = totalDefinitions,
            PartOfSpeechBreakdown = partOfSpeechStats
        };
    }

    private async Task<DictionaryApiResponse?> CallDictionaryApiAsync(string word)
    {
        try
        {
            var url = $"{DictionaryApiBaseUrl}/{word}";
            _logger.LogDebug("Calling dictionary API: {Url}", url);
            
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Dictionary API returned {StatusCode} for word '{Word}'", response.StatusCode, word);
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponses = JsonSerializer.Deserialize<DictionaryApiResponse[]>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return apiResponses?.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling dictionary API for word '{Word}'", word);
            return null;
        }
    }

    private async Task<WordDto> SaveWordToDatabaseAsync(DictionaryApiResponse apiResponse)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Create the Word entity
            var wordEntity = new Word
            {
                Text = apiResponse.Word.ToLowerInvariant(),
                Pronunciation = apiResponse.Phonetic,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedFromApi = DateTime.UtcNow
            };

            _context.Words.Add(wordEntity);
            await _context.SaveChangesAsync(); // Save to get the Word ID

            // Create WordDefinition entities
            var displayOrder = 1;
            foreach (var meaning in apiResponse.Meanings)
            {
                // Find or create the PartOfSpeech
                var partOfSpeech = await _context.PartsOfSpeech
                    .FirstOrDefaultAsync(pos => pos.Name.ToLower() == meaning.PartOfSpeech.ToLower());

                if (partOfSpeech == null)
                {
                    // Create new part of speech if not found
                    partOfSpeech = new PartOfSpeech
                    {
                        Name = meaning.PartOfSpeech,
                        Abbreviation = GetPartOfSpeechAbbreviation(meaning.PartOfSpeech),
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.PartsOfSpeech.Add(partOfSpeech);
                    await _context.SaveChangesAsync();
                }

                // Add definitions for this part of speech
                foreach (var definition in meaning.Definitions)
                {
                    var wordDefinition = new WordDefinition
                    {
                        WordId = wordEntity.Id,
                        PartOfSpeechId = partOfSpeech.Id,
                        Definition = definition.DefinitionText,
                        Example = definition.Example,
                        DisplayOrder = displayOrder++,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.WordDefinitions.Add(wordDefinition);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Successfully saved word '{Word}' to database with {DefinitionCount} definitions", 
                apiResponse.Word, apiResponse.Meanings.SelectMany(m => m.Definitions).Count());

            // Return the saved word
            return await GetWordFromCacheAsync(apiResponse.Word) ?? throw new InvalidOperationException("Failed to retrieve saved word");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error saving word '{Word}' to database", apiResponse.Word);
            throw;
        }
    }

    private static WordDto MapWordToDto(Word word)
    {
        return new WordDto
        {
            Id = word.Id,
            Text = word.Text,
            Pronunciation = word.Pronunciation,
            CreatedAt = word.CreatedAt,
            Definitions = word.WordDefinitions
                .OrderBy(wd => wd.DisplayOrder)
                .Select(wd => new WordDefinitionDto
                {
                    Id = wd.Id,
                    Definition = wd.Definition,
                    Example = wd.Example,
                    PartOfSpeech = wd.PartOfSpeech.Name,
                    PartOfSpeechAbbreviation = wd.PartOfSpeech.Abbreviation,
                    DisplayOrder = wd.DisplayOrder
                })
                .ToList()
        };
    }

    private static string GetPartOfSpeechAbbreviation(string partOfSpeech)
    {
        return partOfSpeech.ToLowerInvariant() switch
        {
            "noun" => "n.",
            "verb" => "v.",
            "adjective" => "adj.",
            "adverb" => "adv.",
            "pronoun" => "pron.",
            "preposition" => "prep.",
            "conjunction" => "conj.",
            "interjection" => "interj.",
            _ => partOfSpeech.Substring(0, Math.Min(3, partOfSpeech.Length)) + "."
        };
    }
}