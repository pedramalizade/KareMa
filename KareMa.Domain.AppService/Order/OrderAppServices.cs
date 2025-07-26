using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;

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

        public Task AcceptOrder(int orderId, CancellationToken cancellationToken)
          => _orderServices.AcceptOrder(orderId, cancellationToken);

       // public async Task AddSampleSuggestionsAsync(int customerId, CancellationToken cancellationToken)
       //=> await _orderServices.AddSampleSuggestionsAsync(customerId, cancellationToken);    

        public async Task<bool> ChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            var suggestionResult = await _suggestionServices.ChangeStatus(status, orderId, cancellationToken);
            if (!suggestionResult)
            {
                Console.WriteLine($"Failed to change suggestion status for OrderId: {orderId}");
                // می‌تونی اینجا تصمیم بگیری ادامه نده یا ادامه بده
            }

            var orderResult = await _orderServices.ChangeStatus(status, orderId, cancellationToken);
            return orderResult; // یا می‌تونی suggestionResult && orderResult برگردونی
        }

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

        public async Task DoneOrder(int id, int suggestionId, CancellationToken cancellationToken)
          => await _orderServices.DoneOrder(id, suggestionId, cancellationToken);//;

        public async Task<List<GetOrderDto>> GetAll(CancellationToken cancellationToken)
          => await _orderServices.GetAll(cancellationToken);

        public async Task<Order> GetById(int orderId, CancellationToken cancellationToken)
          => await _orderServices.GetById(orderId, cancellationToken);

        public async Task<List<GetOrderDto>> GetOrders(int customerId, CancellationToken cancellationToken)
          => await _orderServices.GetOrders(customerId, cancellationToken);

        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByExpertId(int exoertId, CancellationToken cancellationToken)
          => await _orderServices.GetOrdersByExpertId(exoertId, cancellationToken);

        public async Task<int> OrderCount(CancellationToken cancellationToken)
          => await _orderServices.OrderCount(cancellationToken);

        public async Task<bool> OrderIsDone(int orderId, CancellationToken cancellationToken)
          => await _orderServices.OrderIsDone(orderId, cancellationToken);

        public async Task<bool> Update(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
          => await _orderServices.Update(orderUpdateDto, cancellationToken);
    }
}
