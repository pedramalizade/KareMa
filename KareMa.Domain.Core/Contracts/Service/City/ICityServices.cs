namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICityServices
    {
        Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
    }

}
