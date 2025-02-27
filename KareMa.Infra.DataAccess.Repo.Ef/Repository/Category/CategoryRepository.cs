﻿using KareMa.Domain.Core.Contracts.Repositories;
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
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Create(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            if (categoryCreateDto == null || string.IsNullOrEmpty(categoryCreateDto.Name))
            {
                _logger.LogError("Invalid category data.");
                return false;
            }

            var newModel = new Domain.Core.Entities.Category()
            {
                Name = categoryCreateDto.Name,
                Image = string.IsNullOrEmpty(categoryCreateDto.Image) ? "default-image.jpg" : categoryCreateDto.Image,
            };

            await _context.Categories.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var exists = await _context.Categories.AnyAsync(c => c.Name == categoryCreateDto.Name, cancellationToken);
            if (!exists)
            {
                _logger.LogError("Category was not saved to database.");
                return false;
            }

            return true;
            //var newModel = new Domain.Core.Entities.Category()
            //{
            //    Name = categoryCreateDto.Name,
            //    Image = categoryCreateDto.Image,
            //};
            //await _context.Categories.AddAsync(newModel, cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);
            //return true;
        }
        public async Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking().Where(c => c.IsDeleted == false)
                 .Select(s => new CategoryNameDto
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Image = s.Image

                 }).ToListAsync(cancellationToken);
            return categories;
        }
        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.Select(c => new CategoryUpdateDto
            {
                Id = c.Id,
                Image = c.Image,
                Name = c.Name

            }).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        }

        public async Task<bool> Delete(int CategoryId, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceCategory(CategoryId, cancellationToken);
            targetModel.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("category is deleted");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            _logger.LogInformation("category is deleted");
            return true;
        }

        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking()
                .Select(c => new GetCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    IsDeleted = c.IsDeleted
                }).ToListAsync(cancellationToken);
            return categories;
        }

        public async Task<Domain.Core.Entities.Category> GetById(int CategoryId, CancellationToken cancellationToken)
     => await FindServiceCategory(CategoryId, cancellationToken);

        public async Task<bool> Update(CategoryUpdateDto CategoryUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceCategory(CategoryUpdateDto.Id, cancellationToken);

            if (targetModel == null) return false;

            targetModel.Name = CategoryUpdateDto.Name;

            if (CategoryUpdateDto.Image != null)
                targetModel.Image = CategoryUpdateDto.Image;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }


        private async Task<Domain.Core.Entities.Category> FindServiceCategory(int id, CancellationToken cancellationToken)
=> await _context.Categories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
