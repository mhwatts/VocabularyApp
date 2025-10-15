using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Data.Models;

public class UserWord
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int WordId { get; set; }
    // Removed PartOfSpeechId, CustomDefinition, IsFavorite, DifficultyLevel per schema change
    [StringLength(500)]
    public string? PersonalNotes { get; set; } // User's personal notes about the word

    // public bool IsFavorite { get; set; } = false;
    // public int DifficultyLevel { get; set; } = 1; // 1-5 scale for spaced repetition

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastReviewedAt { get; set; }
    public DateTime? LastCorrectAt { get; set; } // For quiz performance tracking

    public int CorrectAnswers { get; set; } = 0;
    public int TotalAttempts { get; set; } = 0;

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Word Word { get; set; } = null!;
    // Removed PartOfSpeech navigation
    public virtual ICollection<SampleSentence> SampleSentences { get; set; } = new List<SampleSentence>();
}