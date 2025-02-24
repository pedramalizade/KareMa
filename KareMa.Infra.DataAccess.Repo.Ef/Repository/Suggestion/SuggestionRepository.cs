using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.SuggestionDTO;
using KareMa.Domain.Core.Enums;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class SuggestionRepository : ISuggestionRepository
    {
        private readonly AppDbContext _context;
        public SuggestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Domain.Core.Entities.Suggestion()
            {
                Description = suggestionCreateDto.Description,
                Expert = suggestionCreateDto.Expert,
                ExpertId = suggestionCreateDto.ExpertId,
                Order = suggestionCreateDto.Order,
                OrderId = suggestionCreateDto.OrderId,
                Price = suggestionCreateDto.Price,
            };
            await _context.Suggestions.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int suggestionId, CancellationToken cancellationToken)
        {
            var targetModel = await FindSuggestion(suggestionId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Domain.Core.Entities.Suggestion>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Suggestions.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Domain.Core.Entities.Suggestion> GetById(int suggestionId, CancellationToken cancellationToken)
        {
            return await FindSuggestion(suggestionId, cancellationToken);
        }

        public async Task<bool> Update(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindSuggestion(suggestionUpdateDto.Id, cancellationToken);

            targetModel.Description = suggestionUpdateDto.Description;
            targetModel.Price = suggestionUpdateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task AcceptSuggestion(int id, CancellationToken cancellationToken)
        {
            var targetSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            var orderId = targetSuggestion.OrderId;

            var otherSuggestions = await _context.Suggestions.Where(s => s.OrderId == orderId).ToListAsync(cancellationToken);

            foreach (var suggestion in otherSuggestions)
            {
                if (suggestion.Id == id)
                {
                    suggestion.Status = StatusEnum.Confirmed;
                }
                else
                {
                    suggestion.Status = StatusEnum.NotConfirmed;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ConfrimedStatusCount(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Suggestions.Where(s => s.OrderId == orderId && s.Status == StatusEnum.Confirmed).CountAsync(cancellationToken);
        }
        private async Task<Domain.Core.Entities.Suggestion> FindSuggestion(int id, CancellationToken cancellationToken)
=> await _context.Suggestions.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
