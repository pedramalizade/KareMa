using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryServices(ISubCategoryRepository serviceSubCategoryRepository)
        {
            _subCategoryRepository = serviceSubCategoryRepository;
        }
        public async Task<bool> Create(SubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.Create(serviceSubCategoryCreateDto, cancellationToken);
        public async Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.Delete(serviceSubCategoryId, cancellationToken);
        public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
          => await _subCategoryRepository.GetAll(cancellationToken);
        public async Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken)
          => await _subCategoryRepository.GetById(serviceSubCategoryId, cancellationToken);
        public async Task<bool> Update(SubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken)
          => await _subCategoryRepository.Update(serviceSubCategoryUpdateDto, cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
      => await _subCategoryRepository.GetCategorisName(cancellationToken);
        public Task<List<GetByCategoryIdDto>> GetAllByCategoryId(int id, CancellationToken cancellationToken)
  => _subCategoryRepository.GetAllByCategoryId(id, cancellationToken);
        public async Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken)
    => await _subCategoryRepository.GetSubCategories(cancellationToken);
        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfo(int id, CancellationToken cancellationToken)
          => await _subCategoryRepository.ServiceSubCategoryUpdateInfo(id, cancellationToken);
    }
}
