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
        public async Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryServices.DeleteAsync(serviceSubCategoryId, cancellationToken);
        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
          => await _subCategoryServices.GetAllAsync(cancellationToken);
        public async Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryServices.GetByIdAsync(serviceSubCategoryId, cancellationToken);
        public async Task<bool> UpdateAsync(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = _baseSevices.UploadImage(image);
            subCategoryUpdateDto.Image = await imageAddress;
            return await _subCategoryServices.UpdateAsync(subCategoryUpdateDto, cancellationToken);
        }
        public async Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken)
  => await _subCategoryServices.GetSubCategoriesAsync(cancellationToken);
        public Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken)
  => _subCategoryServices.GetAllByCategoryIdAsync(id, cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
      => await _subCategoryServices.GetCategorisNameAsync(cancellationToken);
        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
   => await _subCategoryServices.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);
        public async Task<bool> CreateAsync(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken, IFormFile image)
        {
            var imageAddress = _baseSevices.UploadImage(image);
            subCategoryCreateDto.Image = await imageAddress;
            return await _subCategoryServices.CreateAsync(subCategoryCreateDto, cancellationToken);
        }
    }
}
