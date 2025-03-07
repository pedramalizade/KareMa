using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICustomerAppServices
    {
        Task<bool> Create(CustomerCreateDto customerCreateDto,IFormFile Image, CancellationToken cancellationToken);
        Task<bool> Update(CustomerUpdateDto customerUpdateDto, IFormFile Image, CancellationToken cancellationToken);
        Task<bool> Delete(int customerId, CancellationToken cancellationToken);
        Task<Customer> GetById(int customerId, CancellationToken cancellationToken);
        Task<List<GetCustomerDto>> GetAll(CancellationToken cancellationToken);
        Task UpdateBalance(int customerId, decimal newBalance, CancellationToken cancellationToken);
        Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken);
        Task<int> CustomerCount(CancellationToken cancellationToken);
        Task<CustomerUpdateDto> GetCustomerUpdateInfo(int customerId, CancellationToken cancellationToken);
        Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken);
        Task<CustomerUpdateDto> CustomerUpdateInfo(int id, CancellationToken cancellationToken);

    }
}
