namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISuggestionAppServices
    {
        Task<bool> Create(SuggestionCreateDto suggestionCreateDto, string suggestionDate, CancellationToken cancellationToken);
        Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int suggestionId, CancellationToken cancellationToken);
        Task<Entities.Suggestion> GetById(int suggestionId, CancellationToken cancellationToken);
        Task<List<Entities.Suggestion>> GetAll(CancellationToken cancellationToken);
        Task<bool> AcceptSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken);
        Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperId(int id, CancellationToken cancellationToken);
        Task<SuggestionDto> GetSuggestionById(int suggestionId, CancellationToken cancellationToken);
    }
}
