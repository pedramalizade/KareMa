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

        public async Task<bool> Create(ServiceSubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken)
        {

            var newModel = new SubCategory()
            {
                Name = subCategoryCreateDto.Name,
                CategoryId = subCategoryCreateDto.ServiceCategoryId,
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
        {
            return await _context.SubCategories.AsNoTracking().Where(c => c.IsDeleted == false)
                 .Select(s => new SubCategory()
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Image = s.Image,
                     CreatedAt = s.CreatedAt,
                     IsDeleted = s.IsDeleted,
                     Category = s.Category,
                     CategoryId = s.CategoryId,
                     Services = s.Services.Select(x => new Service()
                     {
                         Id = x.Id,
                         Experts = x.Experts,
                         Price = x.Price,
                         Name = x.Name,
                         CreatedAt = x.CreatedAt,
                         IsDeleted = x.IsDeleted,
                         Orders = x.Orders,
                         SubCategory = x.SubCategory,
                         SubCategoryId = x.SubCategoryId
                     }).ToList()
                 })
                 .ToListAsync(cancellationToken);
        }

        public async Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken)
    => await FindServiceSubCategory(serviceSubCategoryId, cancellationToken);

        public async Task<bool> Update(ServiceSubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceSubCategory(serviceSubCategoryUpdateDto.Id, cancellationToken);

            targetModel.Name = serviceSubCategoryUpdateDto.CategoryName;
            if (serviceSubCategoryUpdateDto.Image != null) targetModel.Image = serviceSubCategoryUpdateDto.Image;
            targetModel.CategoryId = serviceSubCategoryUpdateDto.ServiceCategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<SubCategory> FindServiceSubCategory(int id, CancellationToken cancellationToken)
        => await _context.SubCategories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
