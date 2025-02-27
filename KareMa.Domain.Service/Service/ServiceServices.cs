﻿using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Services;

public class ServiceServices : IServiceServices
{
    private readonly IServiceRepository _serviceRepository;
    public ServiceServices(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    public async Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
      => await _serviceRepository.Create(serviceCreateDto, cancellationToken);
    public async Task<bool> Delete(int serviceId, CancellationToken cancellationToken)
       => await _serviceRepository.Delete(serviceId, cancellationToken);
    public async Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken)
         => await _serviceRepository.GetAll(cancellationToken);
    public async Task<Core.Entities.Service> GetById(int serviceId, CancellationToken cancellationToken)
      => await _serviceRepository.GetById(serviceId, cancellationToken);
    public async Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
      => await _serviceRepository.Update(serviceUpdateDto, cancellationToken);
    public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
  => await _serviceRepository.GetAllBySubCategoryId(id, cancellationToken);
    public async Task<List<ServicesNameDto>> GetServicesName(CancellationToken cancellationToken)
      => await _serviceRepository.GetServicesName(cancellationToken);

    public async Task<ServiceNameAndPriceDto> GetServiceNameAndPrice(int id, CancellationToken cancellationToken)
    => await _serviceRepository.GetServiceNameAndPrice(id, cancellationToken);
    public async Task<ServiceUpdateDto> ServiceUpdateInfo(int id, CancellationToken cancellationToken)
      => await _serviceRepository.ServiceUpdateInfo(id, cancellationToken);
}