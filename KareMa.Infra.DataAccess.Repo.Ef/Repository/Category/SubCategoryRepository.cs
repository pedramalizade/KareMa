using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;
        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken)
        {

            var newModel = new SubCategory()
            {
                Name = subCategoryCreateDto.Name,
                CategoryId = subCategoryCreateDto.CategoryId,
                Image = subCategoryCreateDto.Image,
            };
            await _context.SubCategories.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceSubCategory(serviceSubCategoryId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
         => await _context.SubCategories.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<SubCategory> GetById(int SubCategoryId, CancellationToken cancellationToken)
       => await FindServiceSubCategory(SubCategoryId, cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
        {
            var subcategories = await _context.SubCategories.AsNoTracking()
                 .Select(s => new SubCategoryNameDto
                 {
                     Id = s.Id,
                     Name = s.Name,
                 }).ToListAsync(cancellationToken);
            return subcategories;
        }
        public async Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken)
        {
            var subcategories = await _context.SubCategories.AsNoTracking()
                .Select(s => new GetSubCategoryDto
                {
                    Name = s.Name,
                    Id = s.Id,
                    Image = s.Image,
                    Category = s.Category,
                    CategoryId = s.CategoryId,
                    IsDeleted = s.IsDeleted
                }).ToListAsync(cancellationToken);
            return subcategories;
        }

        public async Task<bool> Update(SubCategoryUpdateDto subCategoryUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceSubCategory(subCategoryUpdateDto.Id, cancellationToken);

            targetModel.Name = subCategoryUpdateDto.Name;
            targetModel.Image = subCategoryUpdateDto.Image;
            //targetModel.Services = subCategoryUpdateDto.Services;
            //targetModel.Category = subCategoryUpdateDto.Category;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<SubCategory> FindServiceSubCategory(int id, CancellationToken cancellationToken)
        => await _context.SubCategories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
