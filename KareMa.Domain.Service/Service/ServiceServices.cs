namespace KareMa.Domain.Services;
public class ServiceServices : IServiceServices
{
    private readonly IServiceRepository _serviceRepository;
    public ServiceServices(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    public async Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
      => await _serviceRepository.CreateAsync(serviceCreateDto, cancellationToken);
    public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
       => await _serviceRepository.DeleteAsync(serviceId, cancellationToken);
    public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
         => await _serviceRepository.GetAllAsync(cancellationToken);
    public async Task<Core.Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
      => await _serviceRepository.GetByIdAsync(serviceId, cancellationToken);
    public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
      => await _serviceRepository.UpdateAsync(serviceUpdateDto, cancellationToken);
    public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken)
  => await _serviceRepository.GetAllBySubCategoryIdAsync(id, cancellationToken);
    public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
      => await _serviceRepository.GetServicesNameAsync(cancellationToken);
    public async Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken)
    => await _serviceRepository.GetServiceNameAndPriceAsync(id, cancellationToken);
    public async Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken)
      => await _serviceRepository.ServiceUpdateInfoAsync(id, cancellationToken);
    public async Task<List<Core.Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken)
   => await _serviceRepository.GetAllServicesAsync(cancellationToken);
}