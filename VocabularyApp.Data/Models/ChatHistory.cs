using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class ChatHistory
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty; // "user" or "assistant"
    
    [Required]
    public string Message { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Context { get; set; } // Optional context like "word-lookup", "quiz-help", etc.
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
}