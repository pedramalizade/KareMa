using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using KareMa.Domain.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService
{
    public class OrderAppServices : IOrderAppServices
    {
        private readonly IOrderServices _orderServices;
        private readonly IBaseSevices _baseSevices;
        public OrderAppServices(IOrderServices orderServices, IBaseSevices baseSevices)
        {
            _orderServices = orderServices;
            _baseSevices = baseSevices; 
        }
        public async Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
           => await _orderServices.ChangeStatus(status, orderId, cancellationToken);
        public async Task<bool> Create(OrderCreateDto orderCreateDto, IFormFile image, string runTime, CancellationToken cancellationToken)
        {
            var gregorianDate = _baseSevices.PersianToGregorian(runTime);
            var imageUrl = await _baseSevices.UploadImage(image);
            orderCreateDto.Image = imageUrl;
            orderCreateDto.Date = gregorianDate;
            return await _orderServices.Create(orderCreateDto, cancellationToken);
        }
        public async Task<bool> Delete(int orderId, CancellationToken cancellationToken)
     => await _orderServices.Delete(orderId, cancellationToken);
        public Task AcceptStatus(int orderId, CancellationToken cancellationToken)
    => _orderServices.AcceptStatus(orderId, cancellationToken);
        public async Task<List<GetOrderDto>> GetAll(CancellationToken cancellationToken)
           => await _orderServices.GetAll(cancellationToken);
        public async Task<Order> GetById(int orderId, CancellationToken cancellationToken)
         => await _orderServices.GetById(orderId, cancellationToken);
        public async Task<List<GetOrderDto>> GetOrders(int customerId, CancellationToken cancellationToken)
  => await _orderServices.GetOrders(customerId, cancellationToken);
        public async Task<int> OrderCount(CancellationToken cancellationToken)
      => await _orderServices.OrderCount(cancellationToken);
        public async Task<bool> Update(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderServices.Update(orderUpdateDto, cancellationToken);
    }
}
