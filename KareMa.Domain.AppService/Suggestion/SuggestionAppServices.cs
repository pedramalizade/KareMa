using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService
{
    public class SuggestionAppServices : ISuggestionAppServices
    {
        private readonly ISuggestionServices _suggestionServices;
        private readonly IOrderServices _orderServices;
        public SuggestionAppServices(ISuggestionServices suggestionServices, IOrderServices orderServices)
        {
            _suggestionServices = suggestionServices;
            _orderServices = orderServices;
        }
        public async Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
          => await _suggestionServices.Create(suggestionCreateDto, cancellationToken);
        public async Task<bool> Delete(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionServices.Delete(suggestionId, cancellationToken);
        public async Task<List<Suggestion>> GetAll(CancellationToken cancellationToken)
          => await _suggestionServices.GetAll(cancellationToken);
        public async Task<Suggestion> GetById(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionServices.GetById(suggestionId, cancellationToken);
        public async Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
          => await _suggestionServices.Update(suggestionUpdateDto, cancellationToken);
        public async Task<bool> AcceptSuggestion(int id, int orderid, CancellationToken cancellationToken)
        {
            var confrimedCount = await _suggestionServices.ConfrimedStatusCount(orderid, cancellationToken);
            if (confrimedCount == 0)
            {
                await _suggestionServices.AcceptSuggestion(id, cancellationToken);
                await _orderServices.AcceptStatus(orderid, cancellationToken);
                return true;
            }
            return false;
        }
    }
}
