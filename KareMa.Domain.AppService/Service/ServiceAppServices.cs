namespace KareMa.Domain.AppService
{
    public class ServiceAppServices : IServiceAppServices
    {
        private readonly IServiceServices _serviceServices;
        public ServiceAppServices(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }
        public async Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        => await _serviceServices.CreateAsync(serviceCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
       => await _serviceServices.DeleteAsync(serviceId, cancellationToken);
        public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
      => await _serviceServices.GetAllAsync(cancellationToken);
        public async Task<Core.Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
            => await _serviceServices.GetByIdAsync(serviceId, cancellationToken);
        public async Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken)
  => await _serviceServices.ServiceUpdateInfoAsync(id, cancellationToken);
        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken)
  => await _serviceServices.GetAllBySubCategoryIdAsync(id, cancellationToken);
        public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
    => await _serviceServices.GetServicesNameAsync(cancellationToken);
        public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
    => await _serviceServices.UpdateAsync(serviceUpdateDto, cancellationToken);
        public async Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken)
    => await _serviceServices.GetServiceNameAndPriceAsync(id, cancellationToken);
        public async Task<List<Core.Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        => await _serviceServices.GetAllServicesAsync(cancellationToken);
    }
}
