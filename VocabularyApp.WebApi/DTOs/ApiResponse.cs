// namespace VocabularyApp.WebApi.DTOs
// {
//     public class ApiResponse<T>
//     {
//         public bool Success { get; set; }
//         public string? Error { get; set; }
//         public T? Data { get; set; }

//         public static ApiResponse<T> SuccessResult(T data) => new ApiResponse<T> { Success = true, Data = data };
//         public static ApiResponse<T> ErrorResult(string error) => new ApiResponse<T> { Success = false, Error = error };
//     }

//     public class ApiResponse
//     {
//         public bool Success { get; set; }
//         public string? Error { get; set; }

//         public static ApiResponse SuccessResult() => new ApiResponse { Success = true };
//         public static ApiResponse ErrorResult(string error) => new ApiResponse { Success = false, Error = error };
//     }
// }
