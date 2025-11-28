using KareMa.Domain.Core.DTOs.Expert;
namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IExpertAppServices
    {
        /// <summary>
        /// ایجاد کارشناس جدید با تصویر.
        /// </summary>
        Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات کارشناس و تصویر اختیاری.
        /// </summary>
        Task<bool> UpdateAsync(ExpertUpdateDto expertUpdateDto, IFormFile? Image, CancellationToken cancellationToken);

        /// <summary>
        /// حذف کارشناس بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int expertId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت کارشناس بر اساس شناسه.
        /// </summary>
        Task<Expert> GetByIdAsync(int expertId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه کارشناسان.
        /// </summary>
        Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی موجودی کارشناس.
        /// </summary>
        Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت کارشناس بر اساس شناسه (روش دوم).
        /// </summary>
        Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken);

        /// <summary>
        /// تعداد کل کارشناسان.
        /// </summary>
        Task<int> ExpertCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت خلاصه اطلاعات کارشناس.
        /// </summary>
        Task<ExpertSummaryDto> GetExpertSummaryAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// تعداد نظرات کارشناس.
        /// </summary>
        Task<int> ExpertCommentCountAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// میانگین امتیازات کارشناس.
        /// </summary>
        Task<int> ExpertAverageScoresAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// تعداد سفارش‌های کارشناس.
        /// </summary>
        Task<int> ExpertOrderCountAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی کارشناس.
        /// </summary>
        Task<ExpertUpdateDto> ExpertUpdateInfoAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نام کارشناس.
        /// </summary>
        Task<ExpertNameDto> GetExpertNameAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی کارشناس (روش دوم).
        /// </summary>
        Task<ExpertUpdateDto> GetExpertUpdateAsync(int expertId, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی پروفایل کارشناس با تصویر و تاریخ تولد اختیاری.
        /// </summary>
        Task<bool> UpdateProfileAsync(ExpertUpdateDto expertUpdateDto, IFormFile? image, string? birthDate, CancellationToken cancellationToken);
    }
}
