namespace KareMa.Domain.AppService
{
    public class CityAppServices : ICityAppServices
    {
        private readonly ICityServices _cityServices;

        public CityAppServices(ICityServices cityServices)
        {
            _cityServices = cityServices;
        }

        /// <summary>دریافت همه شهرها.</summary>
        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
            => await _cityServices.GetAllAsync(cancellationToken);

        /// <summary>دریافت شهر با شناسه.</summary>
        public async Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken)
            => await _cityServices.GetByIdAsync(cityId, cancellationToken);
    }
}
