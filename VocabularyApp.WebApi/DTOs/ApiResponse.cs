namespace VocabularyApp.WebApi.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> SuccessResult(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResult(string errorMessage)
    {
        return new ApiResponse<T>
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}

public class ApiResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse SuccessResult()
    {
        return new ApiResponse { Success = true };
    }

    public static ApiResponse ErrorResult(string errorMessage)
    {
        return new ApiResponse
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}