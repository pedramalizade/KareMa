namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IOrderAppServices
    {
        /// <summary>
        /// ایجاد سفارش جدید با تصویر و زمان اجرا.
        /// </summary>
        Task<bool> CreateAsync(OrderCreateDto orderCreateDto, IFormFile image, string runTime, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات سفارش.
        /// </summary>
        Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// حذف سفارش بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken);

        /// <summary>
        /// تغییر وضعیت سفارش.
        /// </summary>
        Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت سفارش بر اساس شناسه.
        /// </summary>
        Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه سفارش‌ها.
        /// </summary>
        Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// تعداد کل سفارش‌ها.
        /// </summary>
        Task<int> OrderCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت سفارش‌های مشتری.
        /// </summary>
        Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken);

        /// <summary>
        /// پذیرش سفارش.
        /// </summary>
        Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken);

        /// <summary>
        /// اتمام سفارش و مرتبط کردن با پیشنهاد.
        /// </summary>
        Task DoneOrderAsync(int id, int suggestionId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت سفارش‌ها بر اساس شناسه کارشناس.
        /// </summary>
        Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int exoertId, CancellationToken cancellationToken);

        /// <summary>
        /// بررسی اتمام سفارش.
        /// </summary>
        Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken);
    }
}
