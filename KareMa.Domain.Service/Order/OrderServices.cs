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
        public async Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.AcceptOrderAsync(orderId, cancellationToken);
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
            => await _orderRepository.ChangeStatusAsync(status, orderId, cancellationToken);
        public async Task<bool> CreateAsync(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
           => await _orderRepository.CreateAsync(orderCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
           => await _orderRepository.DeleteAsync(orderId, cancellationToken);
        public async Task DoneOrderAsync(int orderId, int suggestionId, CancellationToken cancellationToken)
        {
            await _suggestionServices.DoneSuggestionAsync(suggestionId, cancellationToken);
            await _orderRepository.DoneOrderAsync(orderId, cancellationToken);
        }
        public async Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _orderRepository.GetAllAsync(cancellationToken);
        public async Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        public async Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
          => await _orderRepository.GetOrdersAsync(customerId, cancellationToken);
        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int exoertId, CancellationToken cancellationToken)
        {
            var serviceIds = await _expertServices.GetExpertServiceIds(exoertId, cancellationToken);
            return await _orderRepository.GetOrdersByServiceIdsAsync(serviceIds, cancellationToken);
        }
        public async Task<int> OrderCountAsync(CancellationToken cancellationToken)
          => await _orderRepository.OrderCountAsync(cancellationToken);
        public async Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.OrderIsDoneAsync(orderId, cancellationToken);
        public async Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderRepository.UpdateAsync(orderUpdateDto, cancellationToken);
    }
}
