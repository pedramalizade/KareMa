namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICategoryAppServices
    {
        /// <summary>
        /// ایجاد دسته‌بندی جدید با تصویر.
        /// </summary>
        Task<bool> CreateAsync(CategoryCreateDto categoryCreateDto, IFormFile image, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی دسته‌بندی و تصویر (اختیاری).
        /// </summary>
        Task<bool> UpdateAsync(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken);

        /// <summary>
        /// حذف دسته‌بندی بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت دسته‌بندی بر اساس شناسه.
        /// </summary>
        Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه دسته‌بندی‌ها.
        /// </summary>
        Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نام دسته‌بندی‌ها.
        /// </summary>
        Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی دسته‌بندی.
        /// </summary>
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }
}
