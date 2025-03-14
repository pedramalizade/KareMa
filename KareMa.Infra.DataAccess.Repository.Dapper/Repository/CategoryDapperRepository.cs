using Dapper;
using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repository.Dapper.Repository
{
    public class CategoryDapperRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CategoryDapperRepository> _logger;

        public CategoryDapperRepository(IConfiguration configuration, IMemoryCache memoryCache, ILogger<CategoryDapperRepository> logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<bool> Create(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO Categories (Name, Image, IsDeleted) VALUES (@Name, @Image, 0);";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new
                {
                    Name = categoryCreateDto.Name,
                    Image = categoryCreateDto.Image ?? "default-image.jpg"
                });

                if (rowsAffected > 0)
                {
                    _memoryCache.Remove("Categories");
                    return true;
                }

                _logger.LogError("Category was not saved to database.");
                return false;
            }
        }

        public async Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
        {
            var cacheKey = "CategoriesName";
            var categories = _memoryCache.Get<List<CategoryNameDto>>(cacheKey);

            if (categories == null)
            {
                var sql = "SELECT Id, Name, Image FROM Categories WHERE IsDeleted = 0";
                using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
                {
                    categories = (await db.QueryAsync<CategoryNameDto>(sql)).AsList();
                    _memoryCache.Set(cacheKey, categories, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(2000)
                    });
                }
            }

            return categories;
        }

        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, Image FROM Categories WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<CategoryUpdateDto>(sql, new { Id = id });
            }
        }

        public async Task<bool> Delete(int categoryId, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Categories SET IsDeleted = 1 WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                try
                {
                    var rowsAffected = await db.ExecuteAsync(sql, new { Id = categoryId });
                    if (rowsAffected > 0)
                    {
                        _memoryCache.Remove("Categories");
                        _logger.LogInformation("Category is deleted");
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting category with ID: {CategoryId}", categoryId);
                    throw;
                }
            }
        }

        public async Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var cacheKey = "Categories";
            if (!_memoryCache.TryGetValue(cacheKey, out List<GetCategoryDto> categories))
            {
                var sql = "SELECT Id, Name, Image, IsDeleted FROM Categories WHERE IsDeleted = 0";
                using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
                {
                    categories = (await db.QueryAsync<GetCategoryDto>(sql)).AsList();
                    _memoryCache.Set(cacheKey, categories, TimeSpan.FromMinutes(10));
                }
            }
            return categories;
        }

        public async Task<Domain.Core.Entities.Category> GetById(int categoryId, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, Image, IsDeleted FROM Categories WHERE Id = @Id AND IsDeleted = 0";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var category = await db.QueryFirstOrDefaultAsync<Domain.Core.Entities.Category>(sql, new { Id = categoryId });
                if (category == null)
                {
                    _logger.LogInformation($"Category with ID: {categoryId} not found.");
                }
                return category;
            }
        }

        public async Task<bool> Update(CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Categories SET Name = @Name, Image = @Image WHERE Id = @Id AND IsDeleted = 0";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                try
                {
                    var rowsAffected = await db.ExecuteAsync(sql, new
                    {
                        Id = categoryUpdateDto.Id,
                        Name = categoryUpdateDto.Name,
                        Image = categoryUpdateDto.Image
                    });

                    if (rowsAffected > 0)
                    {
                        _memoryCache.Remove("Categories");
                        _logger.LogInformation($"Category with ID: {categoryUpdateDto.Id} updated successfully.");
                        return true;
                    }

                    _logger.LogWarning($"Category with ID: {categoryUpdateDto.Id} not found or not updated.");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating category with ID: {Id}", categoryUpdateDto.Id);
                    return false;
                }
            }
        }
    }
}
