using VocabularyApp.WebApi.DTOs;

namespace VocabularyApp.WebApi.Services;

public interface IWordService
{
    /// <summary>
    /// Looks up a word definition. Checks local database first, then external API if not found.
    /// Stores new words in canonical dictionary for future use.
    /// </summary>
    Task<WordLookupResponse> LookupWordAsync(string word);
    
    /// <summary>
    /// Gets a word from the local database only (no external API call)
    /// </summary>
    Task<WordDto?> GetWordFromCacheAsync(string word);
    
    /// <summary>
    /// Searches for words in the local database by partial text match
    /// </summary>
    Task<List<WordDto>> SearchWordsAsync(string searchTerm, int maxResults = 50);
    
    /// <summary>
    /// Gets word statistics for admin/analytics purposes
    /// </summary>
    Task<object> GetWordStatisticsAsync();
}