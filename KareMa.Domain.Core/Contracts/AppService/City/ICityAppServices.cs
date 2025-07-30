namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICityAppServices
    {
        Task<City> GetById(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAll(CancellationToken cancellationToken);
    }
}
