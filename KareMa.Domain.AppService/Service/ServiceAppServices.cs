namespace KareMa.Domain.AppService
{
    public class ServiceAppServices : IServiceAppServices
    {
        private readonly IServiceServices _serviceServices;
        public ServiceAppServices(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }
        public async Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        => await _serviceServices.Create(serviceCreateDto, cancellationToken);
        public async Task<bool> Delete(int serviceId, CancellationToken cancellationToken)
       => await _serviceServices.Delete(serviceId, cancellationToken);
        public async Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken)
      => await _serviceServices.GetAll(cancellationToken);
        public async Task<Core.Entities.Service> GetById(int serviceId, CancellationToken cancellationToken)
            => await _serviceServices.GetById(serviceId, cancellationToken);
        public async Task<ServiceUpdateDto> ServiceUpdateInfo(int id, CancellationToken cancellationToken)
  => await _serviceServices.ServiceUpdateInfo(id, cancellationToken);
        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
  => await _serviceServices.GetAllBySubCategoryId(id, cancellationToken);
        public async Task<List<ServicesNameDto>> GetServicesName(CancellationToken cancellationToken)
    => await _serviceServices.GetServicesName(cancellationToken);
        public async Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
    => await _serviceServices.Update(serviceUpdateDto, cancellationToken);
        public async Task<ServiceNameAndPriceDto> GetServiceNameAndPrice(int id, CancellationToken cancellationToken)
    => await _serviceServices.GetServiceNameAndPrice(id, cancellationToken);

        public async Task<List<Core.Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        => await _serviceServices.GetAllServicesAsync(cancellationToken);
    }
}
