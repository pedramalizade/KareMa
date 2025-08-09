namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken);
        Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken);
        Task<int> OrderCountAsync(CancellationToken cancellationToken);
        Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken);
        Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken);
        Task DoneOrderAsync(int id, CancellationToken cancellationToken);
        Task<List<OrdersByServiceIdsDto>> GetOrdersByServiceIdsAsync(List<int> serviceIds, CancellationToken cancellationToken);
        Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken);
    }
}
