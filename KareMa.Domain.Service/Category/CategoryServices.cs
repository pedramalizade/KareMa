using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.DataAccess.Repo.Ef.Repository;

namespace KareMa.Domain.Service
{
    public class CategoryServices : ICategoryServices
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = (CategoryRepository?)categoryRepository; // ?
        }
        public async Task<bool> Create(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken)
           => await _categoryRepository.Create(serviceCategoryCreateDto, cancellationToken);
        public async Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken)
           => await _categoryRepository.Delete(serviceCategoryId, cancellationToken);
        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
          => await _categoryRepository.GetAll(cancellationToken);
        public async Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
  => await _categoryRepository.GetCategorisName(cancellationToken);
        public async Task<Category> GetById(int serviceCategoryId, CancellationToken cancellationToken)
          => await _categoryRepository.GetById(serviceCategoryId, cancellationToken);
        public async Task<bool> Update(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken)
          => await _categoryRepository.Update(serviceCategoryUpdateDto, cancellationToken);
    }
}
