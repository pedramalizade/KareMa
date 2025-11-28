namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICustomerAppServices
    {
        /// <summary>
        /// ایجاد مشتری جدید با تصویر.
        /// </summary>
        Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, IFormFile Image, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات مشتری و تصویر.
        /// </summary>
        Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, IFormFile Image, CancellationToken cancellationToken);

        /// <summary>
        /// حذف مشتری بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت مشتری بر اساس شناسه.
        /// </summary>
        Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه مشتری‌ها.
        /// </summary>
        Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی موجودی مشتری.
        /// </summary>
        Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت مشتری بر اساس شناسه (روش دوم).
        /// </summary>
        Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);

        /// <summary>
        /// تعداد کل مشتری‌ها.
        /// </summary>
        Task<int> CustomerCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی مشتری.
        /// </summary>
        Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت خلاصه اطلاعات مشتری.
        /// </summary>
        Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی مشتری (روش دوم).
        /// </summary>
        Task<CustomerUpdateDto> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی پروفایل مشتری با تصویر اختیاری.
        /// </summary>
        Task<OperationResult> UpdateProfileAsync(int userCustomerId, CustomerUpdateDto customerUpdateDto, IFormFile? image, CancellationToken cancellationToken);


    }
}
