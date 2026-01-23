namespace KareMa.Domain.Service
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IExpertServices _expertServices;
        private readonly ISuggestionServices _suggestionServices;

        public OrderServices(IOrderRepository orderRepository, IExpertServices expertServices, ISuggestionServices suggestionServices)
        {
            _orderRepository = orderRepository;
            _expertServices = expertServices;
            _suggestionServices = suggestionServices;
        }
        /// <summary>تأیید سفارش</summary>
        public async Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken)
            => await _orderRepository.AcceptOrderAsync(orderId, cancellationToken);

        /// <summary>تغییر وضعیت سفارش</summary>
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
            => await _orderRepository.ChangeStatusAsync(status, orderId, cancellationToken);

        /// <summary>ایجاد سفارش جدید</summary>
        public async Task<bool> CreateAsync(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
            => await _orderRepository.CreateAsync(orderCreateDto, cancellationToken);

        /// <summary>حذف سفارش</summary>
        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
            => await _orderRepository.DeleteAsync(orderId, cancellationToken);

        /// <summary>اتمام سفارش با ثبت پیشنهاد منتخب</summary>
        public async Task DoneOrderAsync(int orderId, int suggestionId, CancellationToken cancellationToken)
        {
            await _suggestionServices.DoneSuggestionAsync(suggestionId, cancellationToken);
            await _orderRepository.DoneOrderAsync(orderId, cancellationToken);
        }

        /// <summary>دریافت همه سفارش‌ها</summary>
        public async Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken)
            => await _orderRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت سفارش با شناسه</summary>
        public async Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken)
            => await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        /// <summary>دریافت سفارش‌های مشتری</summary>
        public async Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
            => await _orderRepository.GetOrdersAsync(customerId, cancellationToken);

        /// <summary>دریافت سفارش‌های مرتبط با خدمات متخصص</summary>
        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int expertId, CancellationToken cancellationToken)
        {
            var serviceIds = await _expertServices.GetExpertServiceIds(expertId, cancellationToken);
            return await _orderRepository.GetOrdersByServiceIdsAsync(serviceIds, cancellationToken);
        }

        /// <summary>تعداد سفارش‌ها</summary>
        public async Task<int> OrderCountAsync(CancellationToken cancellationToken)
            => await _orderRepository.OrderCountAsync(cancellationToken);

        /// <summary>آیا سفارش انجام شده است؟</summary>
        public async Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken)
            => await _orderRepository.OrderIsDoneAsync(orderId, cancellationToken);

        /// <summary>ویرایش سفارش</summary>
        public async Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
            => await _orderRepository.UpdateAsync(orderUpdateDto, cancellationToken);
    }
}
