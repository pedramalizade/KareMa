using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;

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
        public async Task<bool> Create(CategoryCreateDto categoryCreateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);

            if (string.IsNullOrEmpty(imageAddress))
            {
                Console.WriteLine("Image upload failed!");
                return false;
            }

            categoryCreateDto.Image = imageAddress;
            return await _categoryServices.Create(categoryCreateDto, cancellationToken);

            //var imageAddress = _baseSevices.UploadImage(image);
            //categoryCreateDto.Image = await imageAddress;
            //return await _categoryServices.Create(categoryCreateDto, cancellationToken);
        }
        public async Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken)
           => await _categoryServices.Delete(serviceCategoryId, cancellationToken);
        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
          => await _categoryServices.GetAll(cancellationToken);
        public async Task<Category> GetById(int serviceCategoryId, CancellationToken cancellationToken)
          => await _categoryServices.GetById(serviceCategoryId, cancellationToken);
        public Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
     => _categoryServices.GetCategorisName(cancellationToken);
        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken)
  => await _categoryServices.ServiceCategoryUpdateInfo(id, cancellationToken);
        public async Task<bool> Update(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken)
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

            var result = await _categoryServices.Update(categoryUpdateDto, cancellationToken);
            Console.WriteLine($"CategoryServices.Update result: {result}");
            return result;
        }
    }
}
