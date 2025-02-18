using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Customer()
            {
                FirstName = customerCreateDto.FirstName,
                LastName = customerCreateDto.LastName,
                Gender = customerCreateDto.Gender,
                //PhoneNumber = customerCreateDto.PhoneNumber,
                BackUpPhoneNumber = customerCreateDto.BackUpPhoneNumber,
                BankCardNumber = customerCreateDto.BankCardNumber,
                Addresses = customerCreateDto.Addresses,
            };
            await _context.Customers.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int customerId, CancellationToken cancellationToken)
        {
            var targetModel = await FindCustomer(customerId, cancellationToken);
            targetModel.IsDeleted = true;

            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Customer>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Customers.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetById(int customerId, CancellationToken cancellationToken)
        {
            return await FindCustomer(customerId, cancellationToken);
        }

        public async Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerUpdateDto.Id, cancellationToken)
    ;
            if (targetModel == null)
                return false;

            targetModel.FirstName = customerUpdateDto.FirstName;
            targetModel.LastName = customerUpdateDto.LastName;
            targetModel.Gender = customerUpdateDto.Gender;
            //targetModel.PhoneNumber = customerUpdateDto.PhoneNumber;
            targetModel.BackUpPhoneNumber = customerUpdateDto.BackUpPhoneNumber;
            targetModel.BankCardNumber = customerUpdateDto.BankCardNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken)
        {
            return await _context.Customers.Where(a => a.IsDeleted == false)
                .Select(c => new CustomerSummaryDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BankCardNumber = c.BankCardNumber,
                    BackUpPhoneNumber = c.BackUpPhoneNumber,
                    Gender = c.Gender,
                    Addresses = c.Addresses,
                    Comments = c.Comments,
                    Orders = c.Orders
                }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<int> CustomerCount(CancellationToken cancellationToken)
  => await _context.Customers.CountAsync(cancellationToken);
        private async Task<Customer> FindCustomer(int id, CancellationToken cancellationToken)
     => await _context.Customers.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
