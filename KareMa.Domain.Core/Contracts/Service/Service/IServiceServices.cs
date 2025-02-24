using KareMa.Domain.Core.DTOs.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface IServiceServices
    {
        Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int serviceId, CancellationToken cancellationToken);
        Task<Entities.Service> GetById(int serviceId, CancellationToken cancellationToken);
        Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceUpdateDto> ServiceUpdateInfo(int id, CancellationToken cancellationToken);
        Task<List<ServicesNameDto>> GetServicesName(CancellationToken cancellationToken);
        Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken);
        Task<ServiceNameAndPriceDto> GetServiceNameAndPrice(int id, CancellationToken cancellationToken);


    }
}
