
namespace VocabularyApp.WebApi.Models
{
  public class ApiResult
  {
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public string? Error { get; set; }

    public static ApiResult SuccessResult(object? data = null, string? message = null)
    {
      return new ApiResult
      {
        Success = true,
        Data = data,
        Message = message ?? "Request succeeded."
      };
    }

    public static ApiResult ErrorResult(string message, object? data = null)
    {
      return new ApiResult
      {
        Success = false,
        Message = message,
        Error = message,
        Data = data
      };
    }

    internal static object? ErrorResult(object value)
    {
      throw new NotImplementedException();
    }
  }
}
