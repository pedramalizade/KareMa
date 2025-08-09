namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CreateAsync(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Entities.Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }

}
