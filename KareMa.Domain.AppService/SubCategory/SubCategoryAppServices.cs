using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryServices.Delete(serviceSubCategoryId, cancellationToken);
        public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
          => await _subCategoryServices.GetAll(cancellationToken);
        public async Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryServices.GetById(serviceSubCategoryId, cancellationToken);
        public async Task<bool> Update(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);
            subCategoryUpdateDto.Image = imageAddress;
            return await _subCategoryServices.Update(subCategoryUpdateDto, cancellationToken);
        }
        public async Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken)
  => await _subCategoryServices.GetSubCategories(cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
      => await _subCategoryServices.GetCategorisName(cancellationToken);

        public async Task<bool> Create(SubCategoryCreateDto subCategoryCreateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);
            subCategoryCreateDto.Image = imageAddress;
            return await _subCategoryServices.Create(subCategoryCreateDto, cancellationToken);
        }
    }
}
