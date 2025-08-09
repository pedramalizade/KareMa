namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISubCategoryAppServices
    {
        Task<bool> CreateAsync(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken, IFormFile image);
        Task<bool> UpdateAsync(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetByIdAsync(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<SubCategoryNameDto>> GetCategorisNameAsync(CancellationToken cancellationToken);
        Task<List<GetSubCategoryDto>> GetSubCategoriesAsync(CancellationToken cancellationToken);
        Task<List<GetByCategoryIdDto>> GetAllByCategoryIdAsync(int id, CancellationToken cancellationToken);
        Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfoAsync(int id, CancellationToken cancellationToken);

    }
}
