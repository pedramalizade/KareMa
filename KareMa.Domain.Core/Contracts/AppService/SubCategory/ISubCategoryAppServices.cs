namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISubCategoryAppServices
    {
        Task<bool> Create(SubCategoryCreateDto subCategoryCreateDto, CancellationToken cancellationToken, IFormFile image);
        Task<bool> Update(SubCategoryUpdateDto subCategoryUpdateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<SubCategory> GetById(int serviceSubCategoryId, CancellationToken cancellationToken);
        Task<List<SubCategory>> GetAll(CancellationToken cancellationToken);
        Task<List<SubCategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<List<GetSubCategoryDto>> GetSubCategories(CancellationToken cancellationToken);
        Task<List<GetByCategoryIdDto>> GetAllByCategoryId(int id, CancellationToken cancellationToken);
        Task<SubCategoryUpdateDto> ServiceSubCategoryUpdateInfo(int id, CancellationToken cancellationToken);

    }
}
