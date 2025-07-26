using Dapper;
using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace KareMa.Infra.DataAccess.Repository.Dapper.Repository
{
    public class CityDapperRepository : ICityRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public CityDapperRepository(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
        {
            var cities = _memoryCache.Get<List<City>>("Cities");
            if (cities is null)
            {
                using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
                {
                    cities = (await db.QueryAsync<City>("SELECT * FROM Cities")).AsList();
                    _memoryCache.Set("Cities", cities, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromDays(90)
                    });
                }
            }
            return cities;
        }

        public async Task<City> GetById(int cityId, CancellationToken cancellationToken)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                return await db.QueryFirstOrDefaultAsync<City>("SELECT * FROM Cities WHERE Id = @Id", new { Id = cityId });
            }
        }
    }
}
