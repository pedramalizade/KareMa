using Dapper;
using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repository.Dapper.Repository
{
    public class SubCategoryDapperRepository : ISubCategoryRepository
    {
        private readonly IConfiguration _configuration;

        public SubCategoryDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Create(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO SubCategories (Name, CategoryId, Image, IsDeleted) VALUES (@Name, @CategoryId, @Image, 0)";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new
                {
                    Name = subCategoryCreateDto.Name,
                    CategoryId = subCategoryCreateDto.CategoryId,
                    Image = subCategoryCreateDto.Image
                });
                return rowsAffected > 0;
            }
        }

        public async Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken)
        {
            var sql = "UPDATE SubCategories SET IsDeleted = 1 WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new { Id = serviceSubCategoryId });
                return rowsAffected > 0;
            }
        }

        public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
        {
            var sql = @"
        SELECT sc.Id, sc.Name, sc.Image, sc.CreatedAt, sc.IsDeleted, sc.CategoryId,
               c.Id AS Category_Id, c.Name AS Category_Name, c.Image AS Category_Image, 
               c.CreatedAt AS Category_CreatedAt, c.IsDeleted AS Category_IsDeleted,
               s.Id AS Service_Id, s.Name AS Service_Name, s.Price AS Service_Price, 
               s.Image AS Service_Image, s.CreatedAt AS Service_CreatedAt, 
               s.IsDeleted AS Service_IsDeleted, s.SubCategoryId
        FROM SubCategories sc
        LEFT JOIN Categories c ON sc.CategoryId = c.Id
        LEFT JOIN Services s ON sc.Id = s.SubCategoryId
        WHERE sc.IsDeleted = 0
        ORDER BY sc.Id, c.Id, s.Id"; // برای اطمینان از ترتیب

            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var subCategoryDict = new Dictionary<int, SubCategory>();

                var result = await db.QueryAsync<SubCategory, Category, Service, SubCategory>(
                    sql,
                    (subCategory, category, service) =>
                    {
                        // اگر SubCategory جدیده، به دیکشنری اضافه کن
                        if (!subCategoryDict.TryGetValue(subCategory.Id, out var existingSubCategory))
                        {
                            existingSubCategory = subCategory;
                            existingSubCategory.Services = new List<Service>();
                            subCategoryDict.Add(subCategory.Id, existingSubCategory);
                        }

                        // مپ کردن Category (اگه وجود داره و هنوز ست نشده)
                        if (category != null && existingSubCategory.Category == null)
                        {
                            existingSubCategory.Category = category;
                            // برای جلوگیری از چرخه، SubCategories توی Category رو null می‌کنیم
                            existingSubCategory.Category.SubCategories = null;
                        }

                        // مپ کردن Service (اگه وجود داره و معتبره)
                        if (service != null && service.Id != 0)
                        {
                            // فقط SubCategoryId رو نگه دار تا چرخه ایجاد نشه
                            service.SubCategoryId = existingSubCategory.Id;
                            // ارجاع SubCategory رو حذف کن تا چرخه ایجاد نشه
                            service.SubCategory = null; // این خط مهم است
                                                        // برای Experts و Orders هم می‌تونی null کنی اگه نمی‌خوای لود بشن
                            service.Experts = null;
                            service.Orders = null;
                            existingSubCategory.Services.Add(service);
                        }

                        return existingSubCategory;
                    },
                    splitOn: "Category_Id, Service_Id");

                return subCategoryDict.Values.ToList();
            }
        }

        public async Task<SubCategory> GetById(int subCategoryId, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT sc.Id, sc.Name, sc.Image, sc.CreatedAt, sc.IsDeleted, sc.CategoryId,
                       c.Id AS Category_Id, c.Name AS Category_Name, c.IsDeleted AS Category_IsDeleted
                FROM SubCategories sc
                LEFT JOIN Categories c ON sc.CategoryId = c.Id
                WHERE sc.Id = @Id";

            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                SubCategory subCategory = null;
                await db.QueryAsync<SubCategory, Category, SubCategory>(
                    sql,
                    (sc, category) =>
                    {
                        subCategory ??= sc;
                        subCategory.Category = category;
                        subCategory.Services = new List<Service>();
                        return subCategory;
                    },
                    new { Id = subCategoryId },
                    splitOn: "Category_Id");

                return subCategory;
            }
        }

        public async Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name FROM SubCategories";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return (await db.QueryAsync<SubCategoryNameDto>(sql)).AsList();
            }
        }

        public async Task<List<GetByCategoryIdDto>> GetAllByCategoryId(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Image, Name FROM SubCategories WHERE CategoryId = @CategoryId AND IsDeleted = 0";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return (await db.QueryAsync<GetByCategoryIdDto>(sql, new { CategoryId = id })).AsList();
            }
        }

        public async Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT sc.Id, sc.Name, sc.Image, sc.IsDeleted, sc.CategoryId,
                       c.Id AS Category_Id, c.Name AS Category_Name, c.IsDeleted AS Category_IsDeleted
                FROM SubCategories sc
                LEFT JOIN Categories c ON sc.CategoryId = c.Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var result = await db.QueryAsync<GetSubCategoryDto, Category, GetSubCategoryDto>(
                    sql,
                    (subCategory, category) =>
                    {
                        subCategory.Category = category;
                        return subCategory;
                    },
                    splitOn: "Category_Id");
                return result.AsList();
            }
        }

        public async Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfo(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name AS CategoryName, Image, CategoryId FROM SubCategories WHERE Id = @Id AND IsDeleted = 0";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<SubCategoryUpdateDto>(sql, new { Id = id });
            }
        }

        public async Task<bool> Update(SubCategoryUpdateDto subCategoryUpdateDto, CancellationToken cancellationToken)
        {
            var sql = "UPDATE SubCategories SET Name = @CategoryName, Image = @Image WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new
                {
                    Id = subCategoryUpdateDto.Id,
                    CategoryName = subCategoryUpdateDto.CategoryName,
                    Image = subCategoryUpdateDto.Image
                });
                return rowsAffected > 0;
            }
        }
    }
}