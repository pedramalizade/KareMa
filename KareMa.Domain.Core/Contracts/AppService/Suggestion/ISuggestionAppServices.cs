using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISuggestionAppServices
    {
        Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int suggestionId, CancellationToken cancellationToken);
        Task<Suggestion> GetById(int suggestionId, CancellationToken cancellationToken);
        Task<List<Suggestion>> GetAll(CancellationToken cancellationToken);
        Task<bool> AcceptSuggestion(int id, int orderid, CancellationToken cancellationToken);
    }
}
