using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICityServices
    {
        Task<City> GetById(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAll(CancellationToken cancellationToken);
    }

}
