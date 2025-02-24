using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ISubCategoryServices
    {
        Task<bool> Create(SubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(SubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAll(CancellationToken cancellationToken);
        Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken);
        Task<List<GetByCategoryIdDto>> GetAllByCategoryId(int id, CancellationToken cancellationToken);
        Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfo(int id, CancellationToken cancellationToken);


    }
}
