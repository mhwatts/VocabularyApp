namespace VocabularyApp.WebApi.DTOs
{
  public class WordRequest
  {
    public string Word { get; set; } = string.Empty;
    public string? Pronunciation { get; set; }

    public List<DefinitionDto>? Definitions { get; set; }
  }

  public class DefinitionDto
  {
    public string? PartOfSpeech { get; set; }
    public string? Definition { get; set; }
    public string? Example { get; set; }
  }
}
