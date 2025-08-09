namespace KareMa.Domain.Service.Expect
{
    public class ExpertServices : IExpertServices
    {
        private readonly IExpertRepository _expertRepository;
        public ExpertServices(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }
        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
          => await _expertRepository.CreateAsync(expertCreateDto, cancellationToken);
        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
          => await _expertRepository.DeleteAsync(expertId, cancellationToken);
        public async Task<int> ExpertAverageScores(int id, CancellationToken cancellationToken)
      => await _expertRepository.ExpertAverageScoresAsync(id, cancellationToken);
        public async Task<int> ExpertCommentCount(int id, CancellationToken cancellationToken)
        => await _expertRepository.ExpertCommentCountAsync(id, cancellationToken);
        public async Task<int> ExpertCount(CancellationToken cancellationToken)
      => await _expertRepository.ExpertCountAsync(cancellationToken);
        public async Task<int> ExpertOrderCount(int id, CancellationToken cancellationToken)
      => await _expertRepository.ExpertOrderCountAsync(id, cancellationToken);
        public async Task<ExpertUpdateDto> ExpertUpdateInfo(int id, CancellationToken cancellationToken)
       => await _expertRepository.ExpertUpdateInfoAsync(id,  cancellationToken);
        public async Task<List<int>> GetExpertServiceIds(int id, CancellationToken cancellationToken)
  => await _expertRepository.GetExpertServiceIdsAsync(id, cancellationToken);
        public async Task<ExpertNameDto> GetExpertName(int id, CancellationToken cancellationToken)
          => await _expertRepository.GetExpertNameAsync(id, cancellationToken);
        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
          => await _expertRepository.GetAllAsync(cancellationToken);
        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
          => await _expertRepository.GetByIdAsync(expertId, cancellationToken);
        public async Task<ExpertSummaryDto> GetExpertSummary(int id, CancellationToken cancetionToken)
          => await _expertRepository.GetExpertSummaryAsync(id, cancetionToken);
        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
          => await _expertRepository.UpdateAsync(expertUpdateDto, cancellationToken);
        public async Task<Expert> GetExpertById(int expertId, CancellationToken cancellationToken)
       => await _expertRepository.GetExpertByIdAsync(expertId, cancellationToken);
        public async Task UpdateBalance(int expertId, decimal newBalance, CancellationToken cancellationToken)
            => await _expertRepository.UpdateBalanceAsync(expertId, newBalance, cancellationToken);  
    }
}
