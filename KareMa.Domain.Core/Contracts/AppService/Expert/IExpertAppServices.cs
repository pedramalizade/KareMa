using KareMa.Domain.Core.DTOs.Expert;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IExpertAppServices
    {
        Task<bool> Create(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken);
        Task<bool> Update(ExpertUpdateDto expertUpdateDto, IFormFile? Image, CancellationToken cancellationToken);
        Task<bool> Delete(int expertId, CancellationToken cancellationToken);
        Task<Expert> GetById(int expertId, CancellationToken cancellationToken);
        Task<List<Expert>> GetAll(CancellationToken cancellationToken);
        Task UpdateBalance(int expertId, decimal newBalance, CancellationToken cancellationToken);
        Task<Expert> GetExpertById(int expertId, CancellationToken cancellationToken);
        Task<int> ExpertCount(CancellationToken cancellationToken);
        Task<ExpertSummaryDto> GetExpertSummary(int id, CancellationToken cancellationToken);
        Task<int> ExpertCommentCount(int id, CancellationToken cancellationToken);
        Task<int> ExpertAverageScores(int id, CancellationToken cancellationToken);
        Task<int> ExpertOrderCount(int id, CancellationToken cancellationToken);
        Task<ExpertUpdateDto> ExpertUpdateInfo(int id, CancellationToken cancellationToken);
        Task<ExpertNameDto> GetExpertName(int id, CancellationToken cancellationToken);
    }
}
