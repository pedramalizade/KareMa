namespace KareMa.Domain.Service
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryServices(ISubCategoryRepository serviceSubCategoryRepository)
        {
            _subCategoryRepository = serviceSubCategoryRepository;
        }
        public async Task<bool> CreateAsync(SubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.CreateAsync(serviceSubCategoryCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.DeleteAsync(serviceSubCategoryId, cancellationToken);
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
          => await _subCategoryRepository.GetAllAsync(cancellationToken);
        public async Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.GetByIdAsync(serviceSubCategoryId, cancellationToken);
        public async Task<bool> UpdateAsync(SubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.UpdateAsync(serviceSubCategoryUpdateDto, cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
      => await _subCategoryRepository.GetCategorisNameAsync(cancellationToken);
        public Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken)
  => _subCategoryRepository.GetAllByCategoryIdAsync(id, cancellationToken);
        public async Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken)
    => await _subCategoryRepository.GetSubCategoriesAsync(cancellationToken);
        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
          => await _subCategoryRepository.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);
    }
}
