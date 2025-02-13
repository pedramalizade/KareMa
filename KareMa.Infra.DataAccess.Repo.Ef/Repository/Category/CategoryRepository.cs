using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        //private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
            //_logger = logger;
        }

        public async Task<bool> Create(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Domain.Core.Entities.Category()
            {
                Name = categoryCreateDto.Name,
                Image = categoryCreateDto.Image,
            };
            await _context.Categories.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int CategoryId, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceCategory(CategoryId, cancellationToken);
            targetModel.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                //_logger.LogInformation("category is deleted");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<List<Domain.Core.Entities.Category>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Domain.Core.Entities.Category> GetById(int CategoryId, CancellationToken cancellationToken)
     => await FindServiceCategory(CategoryId, cancellationToken);

        public async Task<bool> Update(CategoryUpdateDto CategoryUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceCategory(CategoryUpdateDto.Id, cancellationToken);

            targetModel.Name = CategoryUpdateDto.Name;

            if (CategoryUpdateDto.Image != null) targetModel.Image = CategoryUpdateDto.Image;

            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }


        private async Task<Domain.Core.Entities.Category> FindServiceCategory(int id, CancellationToken cancellationToken)
=> await _context.Categories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
