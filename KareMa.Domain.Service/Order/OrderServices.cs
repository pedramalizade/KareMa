using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        public OrderServices(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
            => await _orderRepository.ChangeStatus(status, orderId, cancellationToken);
        public async Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
          => await _orderRepository.Create(orderCreateDto, cancellationToken);
        public async Task AcceptStatus(int orderId, CancellationToken cancellationToken)
  => await _orderRepository.AcceptStatus(orderId, cancellationToken);
        public async Task<bool> Delete(int orderId, CancellationToken cancellationToken)
              => await _orderRepository.Delete(orderId, cancellationToken);
        public async Task<List<GetOrderDto>> GetOrders(int customerId, CancellationToken cancellationToken)
  => await _orderRepository.GetOrders(customerId, cancellationToken);
        public async Task<List<GetOrderDto>> GetAll(CancellationToken cancellationToken)
     => await _orderRepository.GetAll(cancellationToken);
        public async Task<Order> GetById(int orderId, CancellationToken cancellationToken)
      => await _orderRepository.GetById(orderId, cancellationToken);
        public async Task<int> OrderCount(CancellationToken cancellationToken)
       => await _orderRepository.OrderCount(cancellationToken);
        public async Task<bool> Update(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderRepository.Update(orderUpdateDto, cancellationToken);
    }
}
