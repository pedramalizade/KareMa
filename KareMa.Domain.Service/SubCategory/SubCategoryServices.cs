namespace KareMa.Domain.Service
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryServices(ISubCategoryRepository serviceSubCategoryRepository)
        {
            _subCategoryRepository = serviceSubCategoryRepository;
        }
        /// <summary>
        /// ایجاد یک زیرشاخه جدید
        /// </summary>
        public async Task<bool> CreateAsync(SubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.CreateAsync(serviceSubCategoryCreateDto, cancellationToken);
        /// <summary>
        /// حذف زیرشاخه بر اساس شناسه
        /// </summary>
        public async Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.DeleteAsync(serviceSubCategoryId, cancellationToken);
        /// <summary>
        /// دریافت تمام زیرشاخه‌ها
        /// </summary>
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
          => await _subCategoryRepository.GetAllAsync(cancellationToken);

        /// <summary>
        /// دریافت زیرشاخه بر اساس شناسه
        /// </summary>
        public async Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.GetByIdAsync(serviceSubCategoryId, cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات زیرشاخه
        /// </summary>
        public async Task<bool> UpdateAsync(SubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.UpdateAsync(serviceSubCategoryUpdateDto, cancellationToken);

        /// <summary>
        /// دریافت نام همه زیرشاخه‌ها
        /// </summary>
        public async Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
      => await _subCategoryRepository.GetCategorisNameAsync(cancellationToken);
        /// <summary>
        /// دریافت تمام زیرشاخه‌ها بر اساس شناسه دسته‌بندی
        /// </summary>
        public Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken)
  => _subCategoryRepository.GetAllByCategoryIdAsync(id, cancellationToken);

        /// <summary>
        /// دریافت لیست DTO همه زیرشاخه‌ها
        /// </summary>
        public async Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken)
    => await _subCategoryRepository.GetSubCategoriesAsync(cancellationToken);
        /// <summary>
        /// دریافت اطلاعات زیرشاخه برای بروزرسانی
        /// </summary>
        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
          => await _subCategoryRepository.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);
    }
}
