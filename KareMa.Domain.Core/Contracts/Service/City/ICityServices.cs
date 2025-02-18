using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICityServices
    {
        Task<City> GetById(int cityId, CancellationToken cancellationToken);
        Task<List<City>> GetAll(CancellationToken cancellationToken);
    }

}
