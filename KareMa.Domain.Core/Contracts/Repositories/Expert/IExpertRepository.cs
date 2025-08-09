namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IExpertRepository
    {
        Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int expertId, CancellationToken cancellationToken);
        Task<Expert> GetByIdAsync(int expertId, CancellationToken cancellationToken);
        Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> ExpertCountAsync(CancellationToken cancellationToken);
        Task<ExpertSummaryDto> GetExpertSummaryAsync(int id, CancellationToken cancellationToken);
        Task<int> ExpertCommentCountAsync(int id, CancellationToken cancellationToken);
        Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken);
        Task<int> ExpertAverageScoresAsync(int id, CancellationToken cancellationToken);
        Task<int> ExpertOrderCountAsync(int id, CancellationToken cancellationToken);
        Task<List<int>> GetExpertServiceIdsAsync(int id, CancellationToken cancellationToken);
        Task<ExpertUpdateDto> ExpertUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken);
        Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken);
        Task<ExpertNameDto> GetExpertNameAsync(int id, CancellationToken cancellationToken);

    }
}
