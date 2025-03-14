using Dapper;
using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.ServiceDTO;
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
    public class ServicedapperRepository : IServiceRepository
    {
        private readonly IConfiguration _configuration;

        public ServicedapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO Services (Name, SubCategoryId, Price, IsDeleted) VALUES (@Name, @SubCategoryId, @Price, 0)";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new
                {
                    Name = serviceCreateDto.Name,
                    SubCategoryId = serviceCreateDto.SubCategoryId,
                    Price = serviceCreateDto.Price
                });
                return rowsAffected > 0;
            }
        }

        public async Task<List<ServicesNameDto>> GetServicesName(CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, Price FROM Services";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return (await db.QueryAsync<ServicesNameDto>(sql)).AsList();
            }
        }

        public async Task<ServiceNameAndPriceDto> GetServiceNameAndPrice(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, Price FROM Services WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<ServiceNameAndPriceDto>(sql, new { Id = id }) ?? new ServiceNameAndPriceDto();
            }
        }

        public async Task<bool> Delete(int serviceId, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Services SET IsDeleted = 1 WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new { Id = serviceId });
                return rowsAffected > 0;
            }
        }

        public async Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT s.Id, s.Name, s.IsDeleted, s.Price, s.SubCategoryId, s.Image,
                       sc.Id as SubCategory_Id, sc.Name as SubCategory_Name 
                FROM Services s
                LEFT JOIN SubCategories sc ON s.SubCategoryId = sc.Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var services = await db.QueryAsync<GetServiceDto, SubCategory, GetServiceDto>(
                    sql,
                    (service, subCategory) =>
                    {
                        service.SubCategory = subCategory;
                        return service;
                    },
                    splitOn: "SubCategory_Id");
                return services.AsList();
            }
        }

        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name FROM Services WHERE SubCategoryId = @SubCategoryId";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return (await db.QueryAsync<GetByCategorySubIdDto>(sql, new { SubCategoryId = id })).AsList();
            }
        }

        public async Task<ServiceUpdateDto> ServiceUpdateInfo(int id, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name AS ServiceName, Price, SubCategoryId FROM Services WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<ServiceUpdateDto>(sql, new { Id = id });
            }
        }

        public async Task<Service> GetById(int serviceId, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, SubCategoryId, Price, IsDeleted, Image FROM Services WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<Service>(sql, new { Id = serviceId });
            }
        }

        public async Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Services SET Name = @ServiceName, Price = @Price WHERE Id = @Id";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var rowsAffected = await db.ExecuteAsync(sql, new
                {
                    Id = serviceUpdateDto.Id,
                    ServiceName = serviceUpdateDto.ServiceName,
                    Price = serviceUpdateDto.Price
                });
                return rowsAffected > 0;
            }
        }

        public async Task<List<Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        {
            var sql = "SELECT Id, Name, SubCategoryId, Price, IsDeleted, Image FROM Services";
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return (await db.QueryAsync<Service>(sql)).AsList();
            }
        }
    }
}
