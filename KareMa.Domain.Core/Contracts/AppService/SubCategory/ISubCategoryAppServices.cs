namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISubCategoryAppServices
    {
        /// <summary>
        /// ایجاد زیردسته جدید با تصویر.
        /// </summary>
        Task<bool> CreateAsync(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken, IFormFile image);

        /// <summary>
        /// بروزرسانی زیردسته و تصویر.
        /// </summary>
        Task<bool> UpdateAsync(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken);

        /// <summary>
        /// حذف زیردسته بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت زیردسته بر اساس شناسه.
        /// </summary>
        Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه زیردسته‌ها.
        /// </summary>
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نام همه زیردسته‌ها.
        /// </summary>
        Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت لیست زیردسته‌ها.
        /// </summary>
        Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت زیردسته‌ها بر اساس شناسه دسته‌بندی.
        /// </summary>
        Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی زیردسته.
        /// </summary>
        Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }
}
