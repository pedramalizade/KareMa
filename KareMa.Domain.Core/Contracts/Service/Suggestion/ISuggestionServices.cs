namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ISuggestionServices
    {
        Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int suggestionId, CancellationToken cancellationToken);
        Task<Suggestion> GetById(int suggestionId, CancellationToken cancellationToken);
        Task<List<Suggestion>> GetAll(CancellationToken cancellationToken);
        Task<bool> AcceptSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken);
        Task<int> ConfrimedStatusCount(int orderId, CancellationToken cancellationToken);
        Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperId(int id, CancellationToken cancellationToken);
        Task DoneSuggestion(int id, CancellationToken cancellationToken);
        Task<SuggestionDto> GetSuggestionById(int suggestionId, CancellationToken cancellationToken);
        Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken);
    }
}
