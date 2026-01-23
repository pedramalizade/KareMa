namespace KareMa.Domain.AppService
{
    public class SubCategoryAppServices : ISubCategoryAppServices
    {
        private readonly ISubCategoryServices _subCategoryServices;
        private readonly IBaseSevices _baseSevices;
        public SubCategoryAppServices(ISubCategoryServices subCategoryServices, IBaseSevices baseSevices)
        {
            _subCategoryServices = subCategoryServices;
            _baseSevices = baseSevices;
        }
        /// <summary>
        /// حذف یک زیرشاخه سرویس.
        /// </summary>
        public async Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
            => await _subCategoryServices.DeleteAsync(serviceSubCategoryId, cancellationToken);

        /// <summary>
        /// دریافت همه زیرشاخه‌ها.
        /// </summary>
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
            => await _subCategoryServices.GetAllAsync(cancellationToken);

        /// <summary>
        /// دریافت زیرشاخه بر اساس شناسه.
        /// </summary>
        public async Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
            => await _subCategoryServices.GetByIdAsync(serviceSubCategoryId, cancellationToken);

        /// <summary>
        /// بروزرسانی زیرشاخه و تصویر آن.
        /// </summary>
        public async Task<bool> UpdateAsync(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = _baseSevices.UploadImage(image);
            subCategoryUpdateDto.Image = await imageAddress;
            return await _subCategoryServices.UpdateAsync(subCategoryUpdateDto, cancellationToken);
        }

        /// <summary>
        /// دریافت همه زیرشاخه‌ها با جزئیات.
        /// </summary>
        public async Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken)
            => await _subCategoryServices.GetSubCategoriesAsync(cancellationToken);

        /// <summary>
        /// دریافت زیرشاخه‌ها بر اساس شناسه دسته‌بندی.
        /// </summary>
        public Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
            => _subCategoryServices.GetAllByCategoryIdAsync(categoryId, cancellationToken);

        /// <summary>
        /// دریافت نام همه زیرشاخه‌ها.
        /// </summary>
        public async Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
            => await _subCategoryServices.GetCategorisNameAsync(cancellationToken);

        /// <summary>
        /// دریافت اطلاعات زیرشاخه برای بروزرسانی.
        /// </summary>
        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int subCategoryId, CancellationToken cancellationToken)
            => await _subCategoryServices.ServiceSubCategoryUpdateInfoAsync(subCategoryId, cancellationToken);

        /// <summary>
        /// ایجاد زیرشاخه جدید با تصویر.
        /// </summary>
        public async Task<bool> CreateAsync(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken, IFormFile image)
        {
            var imageAddress = _baseSevices.UploadImage(image);
            subCategoryCreateDto.Image = await imageAddress;
            return await _subCategoryServices.CreateAsync(subCategoryCreateDto, cancellationToken);
        }
    }
}
