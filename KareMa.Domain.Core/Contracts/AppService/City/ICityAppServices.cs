namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICityAppServices
    {
        /// <summary>
        /// دریافت شهر بر اساس شناسه.
        /// </summary>
        Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه شهرها.
        /// </summary>
        Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
    }
}
