namespace KareMa.Domain.Core.Contracts.Service
{
    public interface IServiceServices
    {
        Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken);
        Task<Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken);
        Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken);
        Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken);
        Task<List<Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken);


    }
}
