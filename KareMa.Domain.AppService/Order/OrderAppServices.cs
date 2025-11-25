namespace KareMa.Domain.AppService
{
    public class OrderAppServices : IOrderAppServices
    {
        private readonly IOrderServices _orderServices;
        private readonly IBaseSevices _baseSevices;
        private readonly ISuggestionServices _suggestionServices;

        public OrderAppServices(IOrderServices orderServices, IBaseSevices baseSevices, ISuggestionServices suggestionServices)
        {
            _orderServices = orderServices;
            _baseSevices = baseSevices;
            _suggestionServices = suggestionServices;
        }
        /// <summary>
        /// تایید سفارش توسط متخصص یا سیستم.
        /// </summary>
        public Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken)
          => _orderServices.AcceptOrderAsync(orderId, cancellationToken);

        /// <summary>
        /// تغییر وضعیت سفارش و بروزرسانی وضعیت پیشنهاد مرتبط.
        /// </summary>
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            var suggestionResult = await _suggestionServices.ChangeStatusAsync(status, orderId, cancellationToken);
            var orderResult = await _orderServices.ChangeStatusAsync(status, orderId, cancellationToken);
            return orderResult;
        }

        /// <summary>
        /// ایجاد سفارش جدید همراه با آپلود تصویر و تبدیل تاریخ.
        /// </summary>
        public async Task<bool> CreateAsync(OrderCreateDto orderCreateDto, IFormFile image, string runTime, CancellationToken cancellationToken)
        {
            var gregorianDate = _baseSevices.PersianToGregorianAsync(runTime);
            var imageUrl = await _baseSevices.UploadImage(image);
            orderCreateDto.Image = imageUrl;
            orderCreateDto.Date = gregorianDate;
            return await _orderServices.CreateAsync(orderCreateDto, cancellationToken);
        }

        /// <summary>
        /// حذف یک سفارش با شناسه مشخص.
        /// </summary>
        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.DeleteAsync(orderId, cancellationToken);

        /// <summary>
        /// ثبت تکمیل شدن سفارش توسط متخصص.
        /// </summary>
        public async Task DoneOrderAsync(int id, int suggestionId, CancellationToken cancellationToken)
          => await _orderServices.DoneOrderAsync(id, suggestionId, cancellationToken);

        /// <summary>
        /// دریافت لیست تمامی سفارش‌ها.
        /// </summary>
        public async Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _orderServices.GetAllAsync(cancellationToken);

        /// <summary>
        /// دریافت سفارش با شناسه مشخص.
        /// </summary>
        public async Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.GetByIdAsync(orderId, cancellationToken);

        /// <summary>
        /// دریافت سفارش‌های یک مشتری.
        /// </summary>
        public async Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
          => await _orderServices.GetOrdersAsync(customerId, cancellationToken);

        /// <summary>
        /// دریافت سفارش‌های یک متخصص.
        /// </summary>
        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int exoertId, CancellationToken cancellationToken)
          => await _orderServices.GetOrdersByExpertIdAsync(exoertId, cancellationToken);

        /// <summary>
        /// دریافت تعداد کل سفارش‌ها.
        /// </summary>
        public async Task<int> OrderCountAsync(CancellationToken cancellationToken)
          => await _orderServices.OrderCountAsync(cancellationToken);

        /// <summary>
        /// بررسی اینکه سفارش انجام شده است یا خیر.
        /// </summary>
        public async Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.OrderIsDoneAsync(orderId, cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات سفارش.
        /// </summary>
        public async Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderServices.UpdateAsync(orderUpdateDto, cancellationToken);
    }
}
