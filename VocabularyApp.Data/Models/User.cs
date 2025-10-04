using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
    public virtual ICollection<SampleSentence> SampleSentences { get; set; } = new List<SampleSentence>();
    public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    public virtual ICollection<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();
}