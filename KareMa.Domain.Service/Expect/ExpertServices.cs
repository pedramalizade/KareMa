namespace KareMa.Domain.Service.Expect
{
    public class ExpertServices : IExpertServices
    {
        private readonly IExpertRepository _expertRepository;
        public ExpertServices(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }
        /// <summary>ایجاد متخصص جدید</summary>
        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
            => await _expertRepository.CreateAsync(expertCreateDto, cancellationToken);

        /// <summary>حذف متخصص</summary>
        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
            => await _expertRepository.DeleteAsync(expertId, cancellationToken);

        /// <summary>میانگین امتیازات متخصص</summary>
        public async Task<int> ExpertAverageScores(int expertAverageScoresId, CancellationToken cancellationToken)
            => await _expertRepository.ExpertAverageScoresAsync(expertAverageScoresId, cancellationToken);

        /// <summary>تعداد کامنت‌های متخصص</summary>
        public async Task<int> ExpertCommentCount(int expertCommentsId, CancellationToken cancellationToken)
            => await _expertRepository.ExpertCommentCountAsync(expertCommentsId, cancellationToken);

        /// <summary>تعداد کل متخصص‌ها</summary>
        public async Task<int> ExpertCount(CancellationToken cancellationToken)
            => await _expertRepository.ExpertCountAsync(cancellationToken);

        /// <summary>تعداد سفارش‌های متخصص</summary>
        public async Task<int> ExpertOrderCount(int expertOrderId, CancellationToken cancellationToken)
            => await _expertRepository.ExpertOrderCountAsync(expertOrderId, cancellationToken);

        /// <summary>اطلاعات ویرایش متخصص</summary>
        public async Task<ExpertUpdateDto> ExpertUpdateInfo(int expertId, CancellationToken cancellationToken)
            => await _expertRepository.ExpertUpdateInfoAsync(expertId, cancellationToken);

        /// <summary>شناسه خدمات متخصص</summary>
        public async Task<List<int>> GetExpertServiceIds(int expertServiceId, CancellationToken cancellationToken)
            => await _expertRepository.GetExpertServiceIdsAsync(expertServiceId, cancellationToken);

        /// <summary>نام متخصص</summary>
        public async Task<ExpertNameDto> GetExpertName(int expertId, CancellationToken cancellationToken)
            => await _expertRepository.GetExpertNameAsync(expertId, cancellationToken);

        /// <summary>دریافت همه متخصص‌ها</summary>
        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
            => await _expertRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت متخصص با شناسه</summary>
        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
            => await _expertRepository.GetByIdAsync(expertId, cancellationToken);

        /// <summary>خلاصه اطلاعات متخصص</summary>
        public async Task<ExpertSummaryDto> GetExpertSummary(int expertSummary, CancellationToken cancellationToken)
            => await _expertRepository.GetExpertSummaryAsync(expertSummary, cancellationToken);

        /// <summary>ویرایش متخصص</summary>
        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
            => await _expertRepository.UpdateAsync(expertUpdateDto, cancellationToken);

        /// <summary>دریافت متخصص با شناسه</summary>
        public async Task<Expert> GetExpertById(int expertId, CancellationToken cancellationToken)
            => await _expertRepository.GetExpertByIdAsync(expertId, cancellationToken);

        /// <summary>به‌روزرسانی موجودی متخصص</summary>
        public async Task UpdateBalance(int expertId, decimal newBalance, CancellationToken cancellationToken)
            => await _expertRepository.UpdateBalanceAsync(expertId, newBalance, cancellationToken);

    }
}
