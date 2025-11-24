namespace KareMa.Domain.Service
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository; 
        }
        /// <summary>ایجاد دسته‌بندی جدید</summary>
        public async Task<bool> CreateAsync(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken)
            => await _categoryRepository.CreateAsync(serviceCategoryCreateDto, cancellationToken);

        /// <summary>حذف دسته‌بندی</summary>
        public async Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken)
            => await _categoryRepository.DeleteAsync(serviceCategoryId, cancellationToken);

        /// <summary>دریافت تمام دسته‌بندی‌ها</summary>
        public async Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
            => await _categoryRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت نام دسته‌بندی‌ها</summary>
        public async Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
            => await _categoryRepository.GetCategorisNameAsync(cancellationToken);

        /// <summary>دریافت اطلاعات جهت ویرایش</summary>
        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
            => await _categoryRepository.ServiceCategoryUpdateInfoAsync(id, cancellationToken);

        /// <summary>دریافت دسته‌بندی با شناسه</summary>
        public async Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken)
            => await _categoryRepository.GetByIdAsync(serviceCategoryId, cancellationToken);

        /// <summary>ویرایش دسته‌بندی</summary>
        public async Task<bool> UpdateAsync(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken)
            => await _categoryRepository.UpdateAsync(serviceCategoryUpdateDto, cancellationToken);
    }
}
