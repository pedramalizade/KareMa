using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken);
    }
}
