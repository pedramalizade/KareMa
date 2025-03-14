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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<bool> Create(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Category()
            {
                Name = categoryCreateDto.Name,
                Image = categoryCreateDto.Image,
            };
            await _context.Categories.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            _memoryCache.Remove("Categories");
            return true;

            //if (categoryCreateDto == null || string.IsNullOrEmpty(categoryCreateDto.Name))
            //{
            //    _logger.LogError("Invalid category data.");
            //    return false;
            //}

            //var newModel = new Domain.Core.Entities.Category()
            //{
            //    Name = categoryCreateDto.Name,
            //    Image = string.IsNullOrEmpty(categoryCreateDto.Image) ? "default-image.jpg" : categoryCreateDto.Image,
            //};

            //await _context.Categories.AddAsync(newModel, cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);

            //var exists = await _context.Categories.AnyAsync(c => c.Name == categoryCreateDto.Name, cancellationToken);
            //if (!exists)
            //{
            //    _logger.LogError("Category was not saved to database.");
            //    return false;
            //}

            //return true;
        }

        public async Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
        {
            var categories = _memoryCache.Get<List<CategoryNameDto>>("CategoriesName");

            if (categories is null)
            {
                categories = await _context.Categories.AsNoTracking().Where(c => c.IsDeleted == false)
                  .Select(s => new CategoryNameDto
                  {
                      Id = s.Id,
                      Name = s.Name,
                      Image = s.Image

                  }).ToListAsync(cancellationToken);
                _memoryCache.Set("CategoriesName", categories, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(2000)
                });
                return categories;
            }

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
            _memoryCache.Remove("Categories");
            return true;
        }

        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            Console.WriteLine("CategoryAppServices.GetAll called");
            var cacheKey = "Categories";
            if (!_memoryCache.TryGetValue(cacheKey, out List<GetCategoryDto> categories))
            {
                categories = await _context.Categories
                    .Where(c => !c.IsDeleted)
                    .Select(c => new GetCategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Image = c.Image,
                        IsDeleted = c.IsDeleted
                    })
                    .ToListAsync(cancellationToken);
                _memoryCache.Set(cacheKey, categories, TimeSpan.FromMinutes(10)); 
            }
            return categories;
        }
        //public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
        //{
        //    var categories = _memoryCache.Get<List<GetCategoryDto>>("AllCategories");
        //    if (categories is null)
        //    {
        //        categories = await _context.Categories.AsNoTracking()
        //           .Select(c => new GetCategoryDto
        //           {
        //               Id = c.Id,
        //               Name = c.Name,
        //               Image = c.Image,
        //               IsDeleted = c.IsDeleted
        //           }).ToListAsync(cancellationToken);
        //        _memoryCache.Set("AllCategories", categories, new MemoryCacheEntryOptions()
        //        {
        //            SlidingExpiration = TimeSpan.FromSeconds(200)
        //        });
        //        return categories;
        //    }

        //    return categories;
        //}

        public async Task<Domain.Core.Entities.Category> GetById(int CategoryId, CancellationToken cancellationToken)
     => await FindServiceCategory(CategoryId, cancellationToken);

        public async Task<bool> Update(CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CategoryRepository.Update started for ID: {categoryUpdateDto.Id}");

            var targetModel = await FindServiceCategory(categoryUpdateDto.Id, cancellationToken);
            if (targetModel == null)
            {
                Console.WriteLine($"Category with ID: {categoryUpdateDto.Id} not found.");
                return false;
            }

            targetModel.Name = categoryUpdateDto.Name ?? targetModel.Name;
            if (categoryUpdateDto.Image != null) targetModel.Image = categoryUpdateDto.Image;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _memoryCache.Remove("Categories");
                Console.WriteLine($"Category with ID: {categoryUpdateDto.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return false;
            }
        }


        private async Task<Domain.Core.Entities.Category> FindServiceCategory(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"FindServiceCategory called with id: {id}");
            var category = await _context.Categories
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken); 
            if (category == null)
            {
                Console.WriteLine($"Category with ID: {id} not found.");
            }
            else
            {
                Console.WriteLine($"Found category with ID: {category.Id}, Name: {category.Name}");
            }
            return category;
        }
    }

}
