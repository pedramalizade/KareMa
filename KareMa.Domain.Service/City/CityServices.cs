namespace KareMa.Domain.Service
{
    public class CityServices : ICityServices
    {
        private readonly ICityRepository _cityRepository;

        public CityServices(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        /// <summary>دریافت تمام شهرها</summary>
        public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
            => await _cityRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت شهر با شناسه</summary>
        public async Task<City> GetByIdAsync(int cityId, CancellationToken cancellationToken)
            => await _cityRepository.GetByIdAsync(cityId, cancellationToken);
    }

}
