namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ServiceRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// ایجاد یک سرویس جدید
        /// </summary>
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

        /// <summary>
        /// دریافت لیست نام و قیمت سرویس‌ها
        /// </summary>
        public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
        {
            return await _context.Services.Select(s => new ServicesNameDto
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price
            }).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// دریافت نام و قیمت یک سرویس بر اساس شناسه
        /// </summary>
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

        /// <summary>
        /// حذف منطقی یک سرویس
        /// </summary>
        public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// دریافت همه سرویس‌ها همراه با زیرشاخه‌ها
        /// </summary>
        public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Queryable
            .Include(s => s.SubCategory)
            .Select(s => new GetServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                IsDeleted = s.IsDeleted,
                Price = s.Price,
                SubCategoryId = s.SubCategoryId,
                Image = s.Image,
                SubCategory = s.SubCategory != null ? new SubCategory { Id = s.SubCategory.Id, Name = s.SubCategory.Name } : null
            }).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// دریافت سرویس‌ها بر اساس شناسه زیرشاخه
        /// </summary>
        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Queryable.Where(x => x.SubCategoryId == id).AsNoTracking()
                .Select(c => new GetByCategorySubIdDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// دریافت اطلاعات یک سرویس برای ویرایش
        /// </summary>
        public async Task<ServiceUpdateDto?> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await Queryable
                .Select(s => new ServiceUpdateDto
                {
                    Id = s.Id,
                    ServiceName = s.Name,
                    Price = s.Price,
                    SubCategoryId = s.SubCategoryId

                }).FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// دریافت یک سرویس بر اساس شناسه
        /// </summary>
        public async Task<Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
       => await FindService(serviceId, cancellationToken);

        /// <summary>
        /// به‌روزرسانی اطلاعات یک سرویس
        /// </summary>
        public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceUpdateDto.Id, cancellationToken);

            targetModel.Name = serviceUpdateDto.ServiceName;
            targetModel.Price = serviceUpdateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        /// <summary>
        /// پیدا کردن یک سرویس بر اساس شناسه
        /// </summary>
        private async Task<Service> FindService(int id, CancellationToken cancellationToken)
       => await Queryable.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        /// <summary>
        /// دریافت همه سرویس‌ها بدون فیلتر
        /// </summary>
        public async Task<List<Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        {
            return await Queryable.ToListAsync(cancellationToken);
        }
    }
}
