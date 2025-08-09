namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CategoryRepository : ICategoryRepository
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

        public async Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
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

        public async Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.Select(c => new CategoryUpdateDto
            {
                Id = c.Id,
                Image = c.Image,
                Name = c.Name

            }).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        }

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
                throw ex;
            }
            _memoryCache.Remove("Categories");
            return true;
        }
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

        public async Task<Domain.Core.Entities.Category> GetByIdAsync(int CategoryId, CancellationToken cancellationToken)
     => await FindServiceCategory(CategoryId, cancellationToken);

        public async Task<bool> UpdateAsync(CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
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
