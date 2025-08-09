namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICityRepository
    {
        Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
    }

}
