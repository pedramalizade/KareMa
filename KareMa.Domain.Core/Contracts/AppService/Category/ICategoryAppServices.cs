namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICategoryAppServices
    {
        Task<bool> CreateAsync(CategoryCreateDto categoryCreateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }
}
