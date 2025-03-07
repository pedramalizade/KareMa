using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
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
        private readonly IExpertServices _expertServices;
        private readonly ISuggestionServices _suggestionServices;

        public OrderServices(IOrderRepository orderRepository, IExpertServices expertServices, ISuggestionServices suggestionServices)
        {
            _orderRepository = orderRepository;
            _expertServices = expertServices;
            _suggestionServices = suggestionServices;
        }

        public async Task AcceptOrder(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.AcceptOrder(orderId, cancellationToken);

        public async Task AddSampleSuggestionsAsync(int customerId, CancellationToken cancellationToken)
       => await _orderRepository.AddSampleSuggestionsAsync(customerId, cancellationToken);

        public async Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
            => await _orderRepository.ChangeStatus(status, orderId, cancellationToken);

        public async Task<bool> Create(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
           => await _orderRepository.Create(orderCreateDto, cancellationToken);

        public async Task<bool> Delete(int orderId, CancellationToken cancellationToken)
           => await _orderRepository.Delete(orderId, cancellationToken);

        public async Task DoneOrder(int orderId, int suggestionId, CancellationToken cancellationToken)
        {
            await _suggestionServices.DoneSuggestion(suggestionId, cancellationToken);
            await _orderRepository.DoneOrder(orderId, cancellationToken);
        }

        public async Task<List<GetOrderDto>> GetAll(CancellationToken cancellationToken)
          => await _orderRepository.GetAll(cancellationToken);

        public async Task<Order> GetById(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.GetById(orderId, cancellationToken);

        public async Task<List<GetOrderDto>> GetOrders(int customerId, CancellationToken cancellationToken)
          => await _orderRepository.GetOrders(customerId, cancellationToken);

        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertId(int exoertId, CancellationToken cancellationToken)
        {
            var serviceIds = await _expertServices.GetExpertServiceIds(exoertId, cancellationToken);
            return await _orderRepository.GetOrdersByServiceIds(serviceIds, cancellationToken);
        }

        public async Task<int> OrderCount(CancellationToken cancellationToken)
          => await _orderRepository.OrderCount(cancellationToken);

        public async Task<bool> OrderIsDone(int orderId, CancellationToken cancellationToken)
          => await _orderRepository.OrderIsDone(orderId, cancellationToken);

        public async Task<bool> Update(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderRepository.Update(orderUpdateDto, cancellationToken);
    }
}
