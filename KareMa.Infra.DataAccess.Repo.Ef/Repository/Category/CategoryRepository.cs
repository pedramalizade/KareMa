namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CategoryRepository : BaseRepository<Category>,  ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, IConfiguration configuration, ILogger<CategoryRepository> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// ایجاد یک دسته‌بندی جدید
        /// </summary>
        public async Task<bool> CreateAsync(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
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
        }

        /// <summary>
        /// دریافت لیست نام دسته‌بندی‌ها از کش یا دیتابیس
        /// </summary>
        public async Task<List<CategoryNameDto>> GetCategoriesNameAsync(CancellationToken cancellationToken)
        {
            var categories = _memoryCache.Get<List<CategoryNameDto>>("CategoriesName");

            if (categories == null)
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

        /// <summary>
        /// دریافت اطلاعات یک دسته‌بندی برای ویرایش
        /// </summary>
        public async Task<CategoryUpdateDto?> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await Queryable
                .Where(c => c.Id == id)
                .Select(c => new CategoryUpdateDto
                {
                    Id = c.Id,
                    Image = c.Image,
                    Name = c.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// حذف منطقی یک دسته‌بندی
        /// </summary>
        public async Task<bool> DeleteAsync(int CategoryId, CancellationToken cancellationToken)
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
                throw new Exception("Fail");
            }
            _memoryCache.Remove("Categories");
            return true;
        }

        /// <summary>
        /// دریافت همه دسته‌بندی‌ها با استفاده از کش
        /// </summary>
        public async Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
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

        /// <summary>
        /// دریافت دسته‌بندی بر اساس شناسه
        /// </summary>
        public async Task<Domain.Core.Entities.Category> GetByIdAsync(int CategoryId, CancellationToken cancellationToken)
     => await FindServiceCategory(CategoryId, cancellationToken);

        /// <summary>
        /// به‌روزرسانی اطلاعات یک دسته‌بندی
        /// </summary>
        public async Task<bool> UpdateAsync(CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ویرایش دسته‌بندی آغاز شد.", categoryUpdateDto.Id);

            var targetModel = await FindServiceCategory(categoryUpdateDto.Id, cancellationToken);
            if (targetModel == null)
            {
                _logger.LogWarning("دسته‌بندی پیدا نشد.", categoryUpdateDto.Id);
                return false;
            }

            targetModel.Name = categoryUpdateDto.Name ?? targetModel.Name;
            if (categoryUpdateDto.Image != null)
            {
                targetModel.Image = categoryUpdateDto.Image;
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _memoryCache.Remove("Categories");

                _logger.LogInformation("دسته‌بندی با موفقیت ویرایش شد.", categoryUpdateDto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ذخیره تغییرات دسته‌بندی ", categoryUpdateDto.Id);
                return false;
            }
        }

        /// <summary>
        /// پیدا کردن یک دسته‌بندی فعال بر اساس شناسه
        /// </summary>
        private async Task<Domain.Core.Entities.Category> FindServiceCategory(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("در حال جستجوی دسته‌بندی", id);

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);

            if (category == null)
            {
                _logger.LogWarning($"دسته‌بندی با شناسه {category.Id} پیدا نشد.", id);
            }
            else
            {
                _logger.LogInformation($"دسته‌بندی یافت شد: شناسه {category.Id} - نام {category.Name}", category.Id, category.Name);
            }

            return category;
        }

        /// <summary>
        /// دریافت لیست نام دسته‌بندی‌ها (Not Implemented)
        /// </summary>
        public Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}