using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class SampleSentence
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public int UserWordId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Sentence { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Context { get; set; } // Optional context about where/why this sentence was created
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual UserWord UserWord { get; set; } = null!;
}