using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICategoryServices
    {

        Task<bool> Create(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Category> GetById(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken);

    }
}
