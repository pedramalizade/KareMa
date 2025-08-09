namespace KareMa.Domain.AppService
{
    public class CategoryAppServices : ICategoryAppServices
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IBaseSevices _baseSevices;
        public CategoryAppServices(ICategoryServices categoryServices, IBaseSevices baseSevices)
        {
            _categoryServices = categoryServices;
            _baseSevices = baseSevices;
        }
        public async Task<bool> CreateAsync(CategoryCreateDto categoryCreateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);

            if (string.IsNullOrEmpty(imageAddress))
            {
                Console.WriteLine("Image upload failed!");
                return false;
            }

            categoryCreateDto.Image = imageAddress;
            return await _categoryServices.CreateAsync(categoryCreateDto, cancellationToken);
        }
        public async Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken)
           => await _categoryServices.DeleteAsync(serviceCategoryId, cancellationToken);
        public async Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _categoryServices.GetAllAsync(cancellationToken);
        public async Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken)
          => await _categoryServices.GetByIdAsync(serviceCategoryId, cancellationToken);
        public Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
     => _categoryServices.GetCategorisNameAsync(cancellationToken);
        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
  => await _categoryServices.ServiceCategoryUpdateInfoAsync(id, cancellationToken);
        public async Task<bool> UpdateAsync(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CategoryAppServices.Update started for ID: {categoryUpdateDto.Id}");

            if (image != null)
            {
                try
                {
                    var imageAddress = await _baseSevices.UploadImage(image); 
                    if (string.IsNullOrEmpty(imageAddress))
                    {
                        Console.WriteLine("Image upload failed.");
                        return false;
                    }
                    categoryUpdateDto.Image = imageAddress;
                    Console.WriteLine($"Image uploaded successfully: {imageAddress}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Image upload error: {ex.Message}");
                    return false;
                }
            }

            var result = await _categoryServices.UpdateAsync(categoryUpdateDto, cancellationToken);
            Console.WriteLine($"CategoryServices.Update result: {result}");
            return result;
        }
    }
}
