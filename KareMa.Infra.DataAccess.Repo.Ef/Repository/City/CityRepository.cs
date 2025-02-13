using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;


        public CityRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
        => await _context.Cities.AsNoTracking().ToListAsync(cancellationToken);


        public async Task<City> GetById(int cityId, CancellationToken cancellationToken)
        {
            return await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
        }
    }
}
