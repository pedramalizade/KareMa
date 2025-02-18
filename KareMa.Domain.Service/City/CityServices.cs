using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service
{
    public class CityServices : ICityServices
    {
        private readonly ICityRepository _cityRepository;

        public CityServices(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
          => await _cityRepository.GetAll(cancellationToken);

        public async Task<City> GetById(int cityId, CancellationToken cancellationToken)
          => await _cityRepository.GetById(cityId, cancellationToken);
    }

}
