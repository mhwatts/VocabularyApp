using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class WordDefinition
{
    public int Id { get; set; }
    
    public int WordId { get; set; }
    public int PartOfSpeechId { get; set; }
    
    [Required]
    [StringLength(1000)]
    public string Definition { get; set; } = string.Empty; // Original definition from external API
    
    [StringLength(500)]
    public string? Example { get; set; } // Example sentence from external API
    
    public int DisplayOrder { get; set; } = 1; // For words with multiple definitions per part of speech
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Word Word { get; set; } = null!;
    public virtual PartOfSpeech PartOfSpeech { get; set; } = null!;
}