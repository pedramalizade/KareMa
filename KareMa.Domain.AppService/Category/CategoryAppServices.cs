using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            categoryCreateDto.Image = imageAddress;
            return await _categoryServices.Create(categoryCreateDto, cancellationToken);
        }
        public async Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken)
           => await _categoryServices.Delete(serviceCategoryId, cancellationToken);
        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
          => await _categoryServices.GetAll(cancellationToken);
        public async Task<Category> GetById(int serviceCategoryId, CancellationToken cancellationToken)
          => await _categoryServices.GetById(serviceCategoryId, cancellationToken);
        public Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
     => _categoryServices.GetCategorisName(cancellationToken);
        public async Task<bool> Update(CategoryUpdateDto categoryUpdateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);
            categoryUpdateDto.Image = imageAddress;
            return await _categoryServices.Update(categoryUpdateDto, cancellationToken);
        }
    }
}
