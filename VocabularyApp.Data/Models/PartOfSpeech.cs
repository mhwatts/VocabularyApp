using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class PartOfSpeech
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty; // noun, verb, adjective, etc.
    
    [StringLength(10)]
    public string Abbreviation { get; set; } = string.Empty; // n., v., adj., etc.
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ICollection<WordDefinition> WordDefinitions { get; set; } = new List<WordDefinition>();
    public virtual ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
}