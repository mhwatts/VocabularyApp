using System.Text.Json.Serialization;

namespace VocabularyApp.WebApi.DTOs.External;

// DTOs for Free Dictionary API (https://dictionaryapi.dev/)
public class DictionaryApiResponse
{
    public string Word { get; set; } = string.Empty;
    public string? Phonetic { get; set; }
    public List<Meaning> Meanings { get; set; } = new();
}

public class Meaning
{
    public string PartOfSpeech { get; set; } = string.Empty;
    public List<Definition> Definitions { get; set; } = new();
}

public class Definition
{
    [JsonPropertyName("definition")]
    public string DefinitionText { get; set; } = string.Empty;
    public string? Example { get; set; }
}