using KareMa.Domain.Core.DTOs.ServiceDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories.Category
{
    public interface IServiceRepository
    {
        Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceId, CancellationToken cancellationToken);
        Task<Service> GetById(int serviceId, CancellationToken cancellationToken);
        Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken);
    }

}
