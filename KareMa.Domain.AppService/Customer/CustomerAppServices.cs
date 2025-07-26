using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace KareMa.Domain.AppService
{
    public class CustomerAppServices : ICustomerAppServices
    {
        private readonly ICustomerServices _customerServices;
        private readonly IBaseSevices _baseSevices;
        public CustomerAppServices(ICustomerServices customerServices, IBaseSevices baseSevices)
        {
            _customerServices = customerServices;
            _baseSevices = baseSevices;
        }
        public async Task<bool> Create(CustomerCreateDto customerCreateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);

            if (string.IsNullOrEmpty(imageAddress))
            {
                Console.WriteLine("Image upload failed!");
                return false;
            }

            customerCreateDto.Image = imageAddress;
            return await _customerServices.Create(customerCreateDto, cancellationToken);
        }
    public async Task<int> CustomerCount(CancellationToken cancellationToken)
          => await _customerServices.CustomerCount(cancellationToken);
        public async Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken)
          => await _customerServices.GetCustomerSummary(id, cancellationToken);
        public async Task<bool> Delete(int customerId, CancellationToken cancellationToken)
          => await _customerServices.Delete(customerId, cancellationToken);
        public async Task<List<GetCustomerDto>> GetAll(CancellationToken cancellationToken)
          => await _customerServices.GetAll(cancellationToken);
        public async Task<Customer> GetById(int customerId, CancellationToken cancellationToken)
          => await _customerServices.GetById(customerId, cancellationToken);
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfo(int customerId, CancellationToken cancellationToken)
  => await _customerServices.GetCustomerUpdateInfo(customerId, cancellationToken);
        public async Task<bool> Update(CustomerUpdateDto customerUpdateDto, IFormFile Image, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CustomerAppServices.Update started for ID: {customerUpdateDto.Id}");

            if (Image != null)
            {
                try
                {
                    var imageAddress = await _baseSevices.UploadImage(Image); // مستقیم await می‌کنیم
                    customerUpdateDto.Image = imageAddress;
                    Console.WriteLine($"Image uploaded: {customerUpdateDto.Image}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Image upload failed: {ex.Message}");
                    return false; // اگه آپلود خطا بده، آپدیت رو متوقف می‌کنیم
                }
            }

            var result = await _customerServices.Update(customerUpdateDto, cancellationToken);
            Console.WriteLine($"CustomerServices.Update result: {result}");
            return result;
        }

        public async Task<CustomerUpdateDto> CustomerUpdateInfo(int id, CancellationToken cancellationToken)
      => await _customerServices.CustomerUpdateInfo(id, cancellationToken);

        public async Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken)
       => await _customerServices.GetCustomerById(customerId, cancellationToken);

        public async Task UpdateBalance(int customerId, decimal newBalance, CancellationToken cancellationToken)
      => await _customerServices.UpdateBalance(customerId, newBalance, cancellationToken);  
    }
}
