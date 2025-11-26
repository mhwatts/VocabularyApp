using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class Word
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Text { get; set; } = string.Empty; // The actual word (e.g., "architect")

    [StringLength(200)]
    public string? Pronunciation { get; set; } // Phonetic pronunciation if available from API

    [StringLength(500)]
    public string? AudioUrl { get; set; } // Audio file URL from dictionary API

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedFromApi { get; set; } // Track when we last fetched from external API

    // Navigation properties
    public virtual ICollection<WordDefinition> WordDefinitions { get; set; } = new List<WordDefinition>();
    public virtual ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
}