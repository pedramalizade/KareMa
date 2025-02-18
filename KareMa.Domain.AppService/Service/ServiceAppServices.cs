using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.Contracts.Service;
using Microsoft.AspNetCore.Http;

namespace KareMa.Domain.AppService
{
    public class ServiceAppServices : IServiceAppServices
    {
        private readonly IServiceServices _serviceServices;
        private readonly IBaseSevices _baseSevices;
        public ServiceAppServices(IServiceServices serviceServices, IBaseSevices baseSevices)
        {
            _serviceServices = serviceServices;
            _baseSevices = baseSevices;
        }
        public async Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken, IFormFile image)
        {
            var imageAddress = await _baseSevices.UploadImage(image);
            serviceCreateDto.Image = imageAddress;
            return await _serviceServices.Create(serviceCreateDto, cancellationToken);
        }
        public async Task<bool> Delete(int serviceId, CancellationToken cancellationToken)
       => await _serviceServices.Delete(serviceId, cancellationToken);
        public async Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken)
      => await _serviceServices.GetAll(cancellationToken);
        public async Task<Core.Entities.Service> GetById(int serviceId, CancellationToken cancellationToken)
            => await _serviceServices.GetById(serviceId, cancellationToken);
        public async Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
    => await _serviceServices.Update(serviceUpdateDto, cancellationToken);
    }
}
