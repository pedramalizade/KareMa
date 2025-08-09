using KareMa.Domain.Core.DTOs.Expert;
namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IExpertAppServices
    {
        Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ExpertUpdateDto expertUpdateDto, IFormFile? Image, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int expertId, CancellationToken cancellationToken);
        Task<Expert> GetByIdAsync(int expertId, CancellationToken cancellationToken);
        Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken);
        Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken);
        Task<int> ExpertCountAsync(CancellationToken cancellationToken);
        Task<ExpertSummaryDto> GetExpertSummaryAsync(int id, CancellationToken cancellationToken);
        Task<int> ExpertCommentCountAsync(int id, CancellationToken cancellationToken);
        Task<int> ExpertAverageScoresAsync(int id, CancellationToken cancellationToken);
        Task<int> ExpertOrderCountAsync(int id, CancellationToken cancellationToken);
        Task<ExpertUpdateDto> ExpertUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<ExpertNameDto> GetExpertNameAsync(int id, CancellationToken cancellationToken);
    }
}
