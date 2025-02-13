using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories.Category
{
    public interface ISubCategoryRepository
    {
        Task<bool> Create(ServiceSubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(ServiceSubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAll(CancellationToken cancellationToken);
    }
}
