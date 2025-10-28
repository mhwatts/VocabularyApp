using VocabularyApp.WebApi.Models;

namespace VocabularyApp.WebApi.Services
{
    public interface IWordService
    {
        Task<ServiceResult<object>> LookupWordAsync(string term);
        Task<ServiceResult<object>> AddWordAsync(AddWordRequest request);
        Task<ServiceResult<object>> AddToVocabularyAsync(int userId, AddWordRequest request);
    }
}
