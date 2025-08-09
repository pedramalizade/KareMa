namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISuggestionAppServices
    {
        Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, string suggestionDate, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken);
        Task<Entities.Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken);
        Task<List<Entities.Suggestion>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken);
        Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperIdAsync(int id, CancellationToken cancellationToken);
        Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken);
    }
}
