using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ISuggestionRepository
    {
        Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int suggestionId, CancellationToken cancellationToken);
        Task<Suggestion> GetById(int suggestionId, CancellationToken cancellationToken);
        Task<List<Suggestion>> GetAll(CancellationToken cancellationToken);
        Task AcceptSuggestion(int id, CancellationToken cancellationToken);
        Task<int> ConfrimedStatusCount(int orderId, CancellationToken cancellationToken);
    }
}
