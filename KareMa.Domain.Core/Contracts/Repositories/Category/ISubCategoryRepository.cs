namespace KareMa.Domain.Core.Contracts.Repositories.Category
{
    public interface ISubCategoryRepository
    {
        Task<bool> CreateAsync(SubCategoryCreateDto serviceSubCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(SubCategoryUpdateDto serviceSubCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);
        Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken);
        Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken);

    }
}
