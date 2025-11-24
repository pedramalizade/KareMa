namespace KareMa.Domain.Services;
public class ServiceServices : IServiceServices
{
    private readonly IServiceRepository _serviceRepository;
    public ServiceServices(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    /// <summary>ایجاد سرویس جدید</summary>
    public async Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        => await _serviceRepository.CreateAsync(serviceCreateDto, cancellationToken);

    /// <summary>حذف سرویس</summary>
    public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
        => await _serviceRepository.DeleteAsync(serviceId, cancellationToken);

    /// <summary>دریافت همه سرویس‌ها</summary>
    public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
        => await _serviceRepository.GetAllAsync(cancellationToken);

    /// <summary>دریافت سرویس با شناسه</summary>
    public async Task<Core.Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
        => await _serviceRepository.GetByIdAsync(serviceId, cancellationToken);

    /// <summary>ویرایش سرویس</summary>
    public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        => await _serviceRepository.UpdateAsync(serviceUpdateDto, cancellationToken);

    /// <summary>دریافت سرویس‌های یک زیرگروه</summary>
    public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken)
        => await _serviceRepository.GetAllBySubCategoryIdAsync(id, cancellationToken);

    /// <summary>دریافت نام سرویس‌ها</summary>
    public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
        => await _serviceRepository.GetServicesNameAsync(cancellationToken);

    /// <summary>دریافت نام و قیمت سرویس</summary>
    public async Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken)
        => await _serviceRepository.GetServiceNameAndPriceAsync(id, cancellationToken);

    /// <summary>اطلاعات ویرایش سرویس</summary>
    public async Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken)
        => await _serviceRepository.ServiceUpdateInfoAsync(id, cancellationToken);

    /// <summary>دریافت لیست کامل سرویس‌ها</summary>
    public async Task<List<Core.Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken)
        => await _serviceRepository.GetAllServicesAsync(cancellationToken);
}