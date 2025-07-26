using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;

namespace KareMa.Domain.Service
{
    public class SuggestionServices : ISuggestionServices
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionServices(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }

        public async Task<bool> AcceptSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken)
        => await _suggestionRepository.AcceptSuggestion(suggestionId, orderId, cancellationToken);

        public async Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ChangeStatus(status, orderId, cancellationToken);

        public async Task<int> ConfrimedStatusCount(int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ConfrimedStatusCount(orderId, cancellationToken);

        public async Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.Create(suggestionCreateDto, cancellationToken);

        public async Task<bool> Delete(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.Delete(suggestionId, cancellationToken);

        public async Task DoneSuggestion(int id, CancellationToken cancellationToken)
          => await _suggestionRepository.DoneSuggestion(id, cancellationToken);

        public async Task<List<Suggestion>> GetAll(CancellationToken cancellationToken)
          => await _suggestionRepository.GetAll(cancellationToken);

        public async Task<Suggestion> GetById(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.GetById(suggestionId, cancellationToken);

        public async Task<SuggestionDto> GetSuggestionById(int suggestionId, CancellationToken cancellationToken)
       => await _suggestionRepository.GetSuggestionById(suggestionId, cancellationToken);

        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperId(int id, CancellationToken cancellationToken)
          => await _suggestionRepository.GetSuggestionsByExperId(id, cancellationToken);

        public async Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.Update(suggestionUpdateDto, cancellationToken);
    }
}
