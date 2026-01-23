namespace KareMa.Domain.AppService
{
    public class ServiceAppServices : IServiceAppServices
    {
        private readonly IServiceServices _serviceServices;
        public ServiceAppServices(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }
        /// <summary>
        /// ایجاد یک سرویس جدید.
        /// </summary>
        public async Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
          => await _serviceServices.CreateAsync(serviceCreateDto, cancellationToken);

        /// <summary>
        /// حذف سرویس با شناسه مشخص.
        /// </summary>
        public async Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken)
          => await _serviceServices.DeleteAsync(serviceId, cancellationToken);

        /// <summary>
        /// دریافت لیست تمام سرویس‌ها.
        /// </summary>
        public async Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _serviceServices.GetAllAsync(cancellationToken);

        /// <summary>
        /// دریافت سرویس با شناسه مشخص.
        /// </summary>
        public async Task<Core.Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken)
          => await _serviceServices.GetByIdAsync(serviceId, cancellationToken);

        /// <summary>
        /// دریافت اطلاعات موردنیاز برای بروزرسانی سرویس.
        /// </summary>
        public async Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int serviceId, CancellationToken cancellationToken)
          => await _serviceServices.ServiceUpdateInfoAsync(serviceId, cancellationToken);

        /// <summary>
        /// دریافت لیست سرویس‌ها بر اساس شناسه زیرمجموعه.
        /// </summary>
        public async Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int subCategoryId, CancellationToken cancellationToken)
          => await _serviceServices.GetAllBySubCategoryIdAsync(subCategoryId, cancellationToken);

        /// <summary>
        /// دریافت نام تمام سرویس‌ها.
        /// </summary>
        public async Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken)
          => await _serviceServices.GetServicesNameAsync(cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات سرویس.
        /// </summary>
        public async Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
          => await _serviceServices.UpdateAsync(serviceUpdateDto, cancellationToken);

        /// <summary>
        /// دریافت نام و قیمت یک سرویس.
        /// </summary>
        public async Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int serviceId, CancellationToken cancellationToken)
          => await _serviceServices.GetServiceNameAndPriceAsync(serviceId, cancellationToken);

        /// <summary>
        /// دریافت تمامی سرویس‌ها بدون فیلتر.
        /// </summary>
        public async Task<List<Core.Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken)
          => await _serviceServices.GetAllServicesAsync(cancellationToken);
    }
}
