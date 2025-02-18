using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICustomerServices
    {
        Task<bool> Create(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int customerId, CancellationToken cancellationToken);
        Task<Customer> GetById(int customerId, CancellationToken cancellationToken);
        Task<List<Customer>> GetAll(CancellationToken cancellationToken);
        Task<int> CustomerCount(CancellationToken cancellationToken);
        Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken);
    }
}
