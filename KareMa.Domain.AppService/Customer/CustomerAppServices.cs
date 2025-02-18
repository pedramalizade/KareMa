using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService
{
    public class CustomerAppServices : ICustomerAppServices
    {
        private readonly ICustomerServices _customerServices;
        public CustomerAppServices(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        public async Task<bool> Create(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
          => await _customerServices.Create(customerCreateDto, cancellationToken);
        public async Task<int> CustomerCount(CancellationToken cancellationToken)
          => await _customerServices.CustomerCount(cancellationToken);
        public async Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken)
          => await _customerServices.GetCustomerSummary(id, cancellationToken);
        public async Task<bool> Delete(int customerId, CancellationToken cancellationToken)
          => await _customerServices.Delete(customerId, cancellationToken);
        public async Task<List<Customer>> GetAll(CancellationToken cancellationToken)
          => await _customerServices.GetAll(cancellationToken);
        public async Task<Customer> GetById(int customerId, CancellationToken cancellationToken)
          => await _customerServices.GetById(customerId, cancellationToken);
        public async Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
          => await _customerServices.Update(customerUpdateDto, cancellationToken);
    }
}
