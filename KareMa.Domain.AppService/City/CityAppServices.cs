namespace KareMa.Domain.AppService
{
    public class CityAppServices : ICityAppServices
    {
        private readonly ICityServices _cityServices;

        public CityAppServices(ICityServices cityServices)
        {
            _cityServices = cityServices;
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
          => await _cityServices.GetAll(cancellationToken);

        public async Task<City> GetById(int cityId, CancellationToken cancellationToken)
          => await _cityServices.GetById(cityId, cancellationToken);
    }
}
