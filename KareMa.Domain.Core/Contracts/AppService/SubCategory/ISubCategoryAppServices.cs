using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISubCategoryAppServices
    {
        Task<bool> Create(SubCategoryCreateDto subCategoryCreateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> Update(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAll(CancellationToken cancellationToken);
        Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken);
    }
}
