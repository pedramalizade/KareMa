namespace KareMa.Domain.Service
{
    public class SuggestionServices : ISuggestionServices
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionServices(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }
        public async Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken)
        => await _suggestionRepository.AcceptSuggestionAsync(suggestionId, orderId, cancellationToken);
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ChangeStatusAsync(status, orderId, cancellationToken);
        public async Task<int> ConfrimedStatusCountAsync(int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ConfrimedStatusCountAsync(orderId, cancellationToken);
        public async Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.CreateAsync(suggestionCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.DeleteAsync(suggestionId, cancellationToken);
        public async Task DoneSuggestionAsync(int id, CancellationToken cancellationToken)
          => await _suggestionRepository.DoneSuggestionAsync(id, cancellationToken);
        public async Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken)
          => await _suggestionRepository.GetAllAsync(cancellationToken);
        public async Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.GetByIdAsync(suggestionId, cancellationToken);
        public async Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
       => await _suggestionRepository.GetSuggestionByIdAsync(suggestionId, cancellationToken);
        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperIdAsync(int id, CancellationToken cancellationToken)
          => await _suggestionRepository.GetSuggestionsByExperIdAsync(id, cancellationToken);
        public async Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.UpdateAsync(suggestionUpdateDto, cancellationToken);
    }
}
