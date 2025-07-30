namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICategoryAppServices
    {
        Task<bool> Create(CategoryCreateDto categoryCreateDto, IFormFile image, CancellationToken cancellationToken);
        Task<bool> Update(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Category> GetById(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken);

    }
}
