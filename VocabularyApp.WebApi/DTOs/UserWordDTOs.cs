using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.WebApi.DTOs;

public class UserWordDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Word { get; set; } = string.Empty;
    public string PartOfSpeech { get; set; } = string.Empty;
    public string? CustomDefinition { get; set; }
    public string? PersonalNotes { get; set; }
    public bool IsFavorite { get; set; }
    public int DifficultyLevel { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime? LastReviewedAt { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalAttempts { get; set; }
    
    // Include the original definition from canonical dictionary
    public string OriginalDefinition { get; set; } = string.Empty;
    public string? OriginalExample { get; set; }
}

public class AddWordToCollectionRequest
{
    [Required]
    public string Word { get; set; } = string.Empty;
    
    [Required]
    public string PartOfSpeech { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? CustomDefinition { get; set; }
    
    [StringLength(500)]
    public string? PersonalNotes { get; set; }
    
    public bool IsFavorite { get; set; } = false;
    
    [Range(1, 5)]
    public int DifficultyLevel { get; set; } = 1;
}

public class UpdateUserWordRequest
{
    [StringLength(1000)]
    public string? CustomDefinition { get; set; }
    
    [StringLength(500)]
    public string? PersonalNotes { get; set; }
    
    public bool IsFavorite { get; set; }
    
    [Range(1, 5)]
    public int DifficultyLevel { get; set; }
}

public class UserWordCollectionResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public List<UserWordDto> Words { get; set; } = new();
    public int TotalCount { get; set; }
}