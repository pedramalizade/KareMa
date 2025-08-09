namespace KareMa.Domain.Service
{
    public class CityServices : ICityServices
    {
        private readonly ICityRepository _cityRepository;

        public CityServices(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
          => await _cityRepository.GetAllAsync(cancellationToken);
        public async Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken)
          => await _cityRepository.GetByIdAsync(cityId, cancellationToken);
    }

}
