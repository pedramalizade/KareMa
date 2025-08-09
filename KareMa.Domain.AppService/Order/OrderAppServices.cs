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
        public Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken)
          => _orderServices.AcceptOrderAsync(orderId, cancellationToken);  
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            var suggestionResult = await _suggestionServices.ChangeStatusAsync(status, orderId, cancellationToken);
            if (!suggestionResult)
            {
                Console.WriteLine($"Failed to change suggestion status for OrderId: {orderId}");
                // می‌تونی اینجا تصمیم بگیری ادامه نده یا ادامه بده
            }

            var orderResult = await _orderServices.ChangeStatusAsync(status, orderId, cancellationToken);
            return orderResult; // یا می‌تونی suggestionResult && orderResult برگردونی
        }
        public async Task<bool> CreateAsync(OrderCreateDto orderCreateDto, IFormFile image, string runTime, CancellationToken cancellationToken)
        {
            var gregorianDate = _baseSevices.PersianToGregorianAsync(runTime);
            var imageUrl = await _baseSevices.UploadImage(image);
            orderCreateDto.Image = imageUrl;
            orderCreateDto.Date = gregorianDate;
            return await _orderServices.CreateAsync(orderCreateDto, cancellationToken);
        }
        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.DeleteAsync(orderId, cancellationToken);
        public async Task DoneOrderAsync(int id, int suggestionId, CancellationToken cancellationToken)
          => await _orderServices.DoneOrderAsync(id, suggestionId, cancellationToken);
        public async Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _orderServices.GetAllAsync(cancellationToken);
        public async Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.GetByIdAsync(orderId, cancellationToken);
        public async Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
          => await _orderServices.GetOrdersAsync(customerId, cancellationToken);
        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int exoertId, CancellationToken cancellationToken)
          => await _orderServices.GetOrdersByExpertIdAsync(exoertId, cancellationToken);
        public async Task<int> OrderCountAsync(CancellationToken cancellationToken)
          => await _orderServices.OrderCountAsync(cancellationToken);
        public async Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken)
          => await _orderServices.OrderIsDoneAsync(orderId, cancellationToken);
        public async Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderServices.UpdateAsync(orderUpdateDto, cancellationToken);
    }
}
