namespace KareMa.Domain.Service
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository; 
        }
        public async Task<bool> CreateAsync(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken)
           => await _categoryRepository.CreateAsync(serviceCategoryCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken)
           => await _categoryRepository.DeleteAsync(serviceCategoryId, cancellationToken);
        public async Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _categoryRepository.GetAllAsync(cancellationToken);
        public async Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
      => await _categoryRepository.GetCategorisNameAsync(cancellationToken);
        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
  => await _categoryRepository.ServiceCategoryUpdateInfoAsync(id, cancellationToken);
        public async Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken)
          => await _categoryRepository.GetByIdAsync(serviceCategoryId, cancellationToken);
        public async Task<bool> UpdateAsync(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken)
          => await _categoryRepository.UpdateAsync(serviceCategoryUpdateDto, cancellationToken);
    }
}
