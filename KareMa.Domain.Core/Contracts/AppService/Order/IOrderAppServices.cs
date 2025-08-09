namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IOrderAppServices
    {
        Task<bool> CreateAsync(OrderCreateDto orderCreateDto, IFormFile image, string runTime, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken);
        Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken);
        Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> OrderCountAsync(CancellationToken cancellationToken);
        Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken);
        Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken);
        Task DoneOrderAsync(int id, int suggestionId, CancellationToken cancellationToken);
        Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertIdAsync(int exoertId, CancellationToken cancellationToken);
        Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken);
    }
}
