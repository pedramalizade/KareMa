namespace KareMa.Domain.Core.Contracts
{
    public interface IServiceAppServices
    {
        /// <summary>
        /// ایجاد سرویس جدید.
        /// </summary>
        Task<bool> CreateAsync(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات سرویس.
        /// </summary>
        Task<bool> UpdateAsync(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// حذف سرویس بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int serviceId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت سرویس بر اساس شناسه.
        /// </summary>
        Task<Entities.Service> GetByIdAsync(int serviceId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه سرویس‌ها.
        /// </summary>
        Task<List<GetServiceDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه سرویس‌ها بر اساس شناسه زیردسته.
        /// </summary>
        Task<List<GetByCategorySubIdDto>> GetAllBySubCategoryIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی سرویس.
        /// </summary>
        Task<ServiceUpdateDto> ServiceUpdateInfoAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نام همه سرویس‌ها.
        /// </summary>
        Task<List<ServicesNameDto>> GetServicesNameAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه سرویس‌ها (نسخه کامل موجودیت).
        /// </summary>
        Task<List<Entities.Service>> GetAllServicesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نام و قیمت سرویس بر اساس شناسه.
        /// </summary>
        Task<ServiceNameAndPriceDto> GetServiceNameAndPriceAsync(int id, CancellationToken cancellationToken);

    }
}
