namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ISuggestionServices
    {
        Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken);
        Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken);
        Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken);
        Task<int> ConfrimedStatusCountAsync(int orderId, CancellationToken cancellationToken);
        Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExpertIdAsync(int id, CancellationToken cancellationToken);
        Task DoneSuggestionAsync(int id, CancellationToken cancellationToken);
        Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken);
        Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken);
    }
}
