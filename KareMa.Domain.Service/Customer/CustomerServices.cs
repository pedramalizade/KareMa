using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Service
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<bool> Create(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
          => _customerRepository.Create(customerCreateDto, cancellationToken);
        public Task<int> CustomerCount(CancellationToken cancellationToken)
          => _customerRepository.CustomerCount(cancellationToken);
        public Task<bool> Delete(int customerId, CancellationToken cancellationToken)
          => _customerRepository.Delete(customerId, cancellationToken);
        public Task<List<Customer>> GetAll(CancellationToken cancellationToken)
          => _customerRepository.GetAll(cancellationToken);
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfo(int customerId, CancellationToken cancellationToken)
   => await _customerRepository.GetCustomerUpdateInfo(customerId, cancellationToken);
        public Task<Customer> GetById(int customerId, CancellationToken cancellationToken)
          => _customerRepository.GetById(customerId, cancellationToken);
        public async Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken)
       => await _customerRepository.GetCustomerSummary(id, cancellationToken);
        public async Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
          => await _customerRepository.Update(customerUpdateDto, cancellationToken);
    }
}
