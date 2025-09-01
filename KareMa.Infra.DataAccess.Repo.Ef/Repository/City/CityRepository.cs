namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public CityRepository(AppDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        {
            var cities = _memoryCache.Get<List<City>>("Cities");
            if (cities is null)
            {
                cities = await _context.Cities.AsNoTracking().ToListAsync(cancellationToken);
                _memoryCache.Set("Cities", cities, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(90)
                });
            }

            return cities;
        }
        public async Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken)
        {
            return await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
        }
    }
}
