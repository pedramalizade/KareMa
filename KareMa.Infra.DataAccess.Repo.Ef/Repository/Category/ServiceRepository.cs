namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class ServiceRepository : IServiceRepository
    
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ServiceRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        {

            var newModel = new Service()
            {
                Name = serviceCreateDto.Name,
                SubCategoryId = serviceCreateDto.SubCategoryId,
                Price = serviceCreateDto.Price,
            };
            await _context.Services.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
        public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
        {
            return await _context.Services.Select(s => new ServicesNameDto
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price
            }).ToListAsync(cancellationToken);
        }
        public async Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken)
        {
            var targetSrtvice = await _context.Services.AsNoTracking().Where(s => s.Id == id)
                  .Select(s => new ServiceNameAndPriceDto
                  {
                      Id = s.Id,
                      Name = s.Name,
                      Price = s.Price,
                  }).FirstOrDefaultAsync(cancellationToken);

            if (targetSrtvice != null) return targetSrtvice;

            return new ServiceNameAndPriceDto();

        }

        public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var sql = @"
        SELECT 
            s.Id, 
            s.Name, 
            s.IsDeleted, 
            s.Price, 
            s.SubCategoryId, 
            s.Image,
            sc.Id AS SubCategoryId, 
            sc.Name AS SubCategoryName 
        FROM Services s
        LEFT JOIN SubCategories sc ON s.SubCategoryId = sc.Id";

            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var services = await db.QueryAsync<GetServiceDto, SubCategory, GetServiceDto>(
                    sql,
                    (service, subCategory) =>
                    {
                        service.SubCategory = subCategory != null
                            ? new SubCategory
                            {
                                Id = subCategory.Id,
                                Name = subCategory.Name
                            }
                            : null;
                        return service;
                    },
                    splitOn: "SubCategoryId");

                return services.AsList();
            }
        }

        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Services.Where(x => x.SubCategoryId == id).AsNoTracking()
                .Select(c => new GetByCategorySubIdDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Services
                .Select(s => new ServiceUpdateDto
                {
                    Id = s.Id,
                    ServiceName = s.Name,
                    Price = s.Price,
                    SubCategoryId = s.SubCategoryId

                }).FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
       => await FindService(serviceId, cancellationToken);

        public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceUpdateDto.Id, cancellationToken);

            targetModel.Name = serviceUpdateDto.ServiceName;
            targetModel.Price = serviceUpdateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task<Service> FindService(int id, CancellationToken cancellationToken)
       => await _context.Services.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<List<Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        {
            return await _context.Services.ToListAsync(cancellationToken);
        }
    }
}
