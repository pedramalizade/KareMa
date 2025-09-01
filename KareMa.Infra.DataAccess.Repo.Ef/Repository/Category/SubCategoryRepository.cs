namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public SubCategoryRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CreateAsync(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken)
        {

            var newModel = new SubCategory()
            {
                Name = subCategoryCreateDto.Name,
                CategoryId = subCategoryCreateDto.CategoryId,
                Image = subCategoryCreateDto.Image,
            };
            await _context.SubCategories.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceSubCategory(serviceSubCategoryId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
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
ORDER BY sc.Id, c.Id, s.Id";

            using (IDbConnection db = new SqlConnection(_configuration.GetSection("ConnectionStrings").Value))
            {
                var subCategoryDict = new Dictionary<int, SubCategory>();

                var result = await db.QueryAsync<SubCategory, Category, Service, SubCategory>(
                    sql,
                    (subCategory, category, service) =>
                    {
                        if (!subCategoryDict.TryGetValue(subCategory.Id, out var existingSubCategory))
                        {
                            existingSubCategory = subCategory;
                            existingSubCategory.Services = new List<Service>();
                            subCategoryDict.Add(subCategory.Id, existingSubCategory);
                        }

                        if (category != null && existingSubCategory.Category == null)
                        {
                            existingSubCategory.Category = category;
                            existingSubCategory.Category.SubCategories = null; 
                        }

                        if (service != null && service.Id != 0)
                        {
                            service.SubCategoryId = existingSubCategory.Id;
                            service.SubCategory = null; 
                            service.Experts = null;
                            service.Orders = null;
                            existingSubCategory.Services.Add(service);
                        }

                        return existingSubCategory;
                    },
                    splitOn: "Category_Id, Service_Id");

                var subCategories = subCategoryDict.Values.ToList();

                foreach (var sc in subCategories)
                {
                    if (sc.Image == null) Console.WriteLine($"SubCategory {sc.Id} has null Image");
                    if (sc.Category == null) Console.WriteLine($"SubCategory {sc.Id} has null Category");
                    if (!sc.Services.Any()) Console.WriteLine($"SubCategory {sc.Id} has no Services");
                }

                return subCategories;
            }
        }

        public async Task<SubCategory> GetByIdAsync(int SubCategoryId, CancellationToken cancellationToken)
       => await FindServiceSubCategory(SubCategoryId, cancellationToken);
        public async Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken)
        {
            var subcategories = await _context.SubCategories.AsNoTracking()
                 .Select(s => new SubCategoryNameDto
                 {
                     Id = s.Id,
                     Name = s.Name,
                 }).ToListAsync(cancellationToken);
            return subcategories;
        }

        public async Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.SubCategories.Where(x => x.CategoryId == id && x.IsDeleted == false).AsNoTracking()
                .Select(c => new GetByCategoryIdDto
                {
                    Id = c.Id,
                    Image = c.Image,
                    Name = c.Name
                })
                .ToListAsync(cancellationToken);
        }
        public async Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken)
        {
            var subcategories = await _context.SubCategories.AsNoTracking()
                .Select(s => new GetSubCategoryDto
                {
                    Name = s.Name,
                    Id = s.Id,
                    Image = s.Image,
                    Category = s.Category,
                    CategoryId = s.CategoryId,
                    IsDeleted = s.IsDeleted
                }).ToListAsync(cancellationToken);
            return subcategories;
        }

        public async Task<SubCategoryUpdateDto?> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.SubCategories.AsNoTracking().Where(c => c.IsDeleted == false)
                .Select(s => new SubCategoryUpdateDto
                {
                    Id = s.Id,
                    CategoryName = s.Name ,
                    Image = s.Image,
                    CategoryId = s.CategoryId

                }).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(SubCategoryUpdateDto subCategoryUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindServiceSubCategory(subCategoryUpdateDto.Id, cancellationToken);

            targetModel.Name = subCategoryUpdateDto.CategoryName;
            targetModel.Image = subCategoryUpdateDto.Image;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<SubCategory> FindServiceSubCategory(int id, CancellationToken cancellationToken)
        => await _context.SubCategories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
