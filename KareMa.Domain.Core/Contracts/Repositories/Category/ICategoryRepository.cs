namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> Create(CategoryCreateDto serviceCategoryCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CategoryUpdateDto serviceCategoryUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceCategoryId, CancellationToken cancellationToken);
        Task<Entities.Category> GetById(int serviceCategoryId, CancellationToken cancellationToken);
        Task<List<GetCategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryNameDto>> GetCategorisName(CancellationToken cancellationToken);
        Task<CategoryUpdateDto> ServiceCategoryUpdateInfo(int id, CancellationToken cancellationToken);

    }

}
