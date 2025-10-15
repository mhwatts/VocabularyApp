using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.WebApi.DTOs;

public class UserWordDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Word { get; set; } = string.Empty;
    public string PartOfSpeech { get; set; } = string.Empty;
<<<<<<< HEAD
    public string? CustomDefinition { get; set; }
    public string? PersonalNotes { get; set; }
    public bool IsFavorite { get; set; }
    public int DifficultyLevel { get; set; }
=======
    public string? PersonalNotes { get; set; }
    // CustomDefinition, IsFavorite, DifficultyLevel removed from model - not exposed here anymore
>>>>>>> fork/feat/remove-userword-fields
    public DateTime AddedAt { get; set; }
    public DateTime? LastReviewedAt { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalAttempts { get; set; }
<<<<<<< HEAD
    
=======

>>>>>>> fork/feat/remove-userword-fields
    // Include the original definition from canonical dictionary
    public string OriginalDefinition { get; set; } = string.Empty;
    public string? OriginalExample { get; set; }
}

public class AddWordToCollectionRequest
{
    [Required]
    public string Word { get; set; } = string.Empty;
<<<<<<< HEAD
    
    [Required]
    public string PartOfSpeech { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? CustomDefinition { get; set; }
    
    [StringLength(500)]
    public string? PersonalNotes { get; set; }
    
    public bool IsFavorite { get; set; } = false;
    
    [Range(1, 5)]
    public int DifficultyLevel { get; set; } = 1;
=======

    [Required]
    public string PartOfSpeech { get; set; } = string.Empty;

    [StringLength(500)]
    public string? PersonalNotes { get; set; }
>>>>>>> fork/feat/remove-userword-fields
}

public class UpdateUserWordRequest
{
<<<<<<< HEAD
    [StringLength(1000)]
    public string? CustomDefinition { get; set; }
    
    [StringLength(500)]
    public string? PersonalNotes { get; set; }
    
    public bool IsFavorite { get; set; }
    
    [Range(1, 5)]
    public int DifficultyLevel { get; set; }
=======
    [StringLength(500)]
    public string? PersonalNotes { get; set; }
>>>>>>> fork/feat/remove-userword-fields
}

public class UserWordCollectionResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public List<UserWordDto> Words { get; set; } = new();
    public int TotalCount { get; set; }
}