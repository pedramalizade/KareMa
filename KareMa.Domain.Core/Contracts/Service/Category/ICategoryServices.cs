namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICategoryServices
    {

        Task<bool> CreateAsync(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategoriesNameAsync(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }
}
