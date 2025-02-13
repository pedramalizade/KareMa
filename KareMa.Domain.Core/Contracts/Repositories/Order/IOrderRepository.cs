using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int orderId, CancellationToken cancellationToken);
        Task<Order> GetById(int orderId, CancellationToken cancellationToken);
        Task<List<GetOrderDto>> GetAll(CancellationToken cancellationToken);

    }
}
