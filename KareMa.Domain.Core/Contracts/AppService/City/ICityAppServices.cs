namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICityAppServices
    {
        Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
    }
}
