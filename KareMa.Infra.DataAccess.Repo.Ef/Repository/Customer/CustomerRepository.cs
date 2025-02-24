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
                PhoneNumber = customerCreateDto.PhoneNumber,
                //BackUpPhoneNumber = customerCreateDto.BackUpPhoneNumber,
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
            //targetModel.Gender = customerUpdateDto.Gender;
            //targetModel.PhoneNumber = customerUpdateDto.PhoneNumber;
            //targetModel.BackUpPhoneNumber = customerUpdateDto.BackUpPhoneNumber;
            //targetModel.BankCardNumber = customerUpdateDto.BankCardNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken)
        {
            var target = await _context.Customers.Where(a => a.Id == id && a.IsDeleted == false)
                .Select(c => new CustomerSummaryDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BankCardNumber = c.BankCardNumber,
                    PhoneNumber = c.PhoneNumber,
                    Gender = c.Gender,
                    Addresses = c.Addresses,
                    Comments = c.Comments,
                    Orders = c.Orders
                }).FirstOrDefaultAsync(cancellationToken);
            if (target is not null)
            {
                return target;
            }
            return new CustomerSummaryDto();
        }
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfo(int customerId, CancellationToken cancellationToken)
        {
            var targetCustomer = await _context.Customers.AsNoTracking().Include(c => c.Addresses)
                 .Select(c => new CustomerUpdateDto()
                 {
                     Id = c.Id,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     PhoneNumber = c.PhoneNumber,
                     Address = c.Addresses,
                 }).FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

            if (targetCustomer is null)
                return new CustomerUpdateDto();

            return targetCustomer;
        }

        public async Task<int> FindCustomerIdWithApplicationUser(int appUserId, CancellationToken cancellationToken)
        {
            var targetCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.AppUserId == appUserId, cancellationToken);
            var customerId = targetCustomer.Id;
            return customerId;
        }
        public async Task<int> CustomerCount(CancellationToken cancellationToken)
  => await _context.Customers.CountAsync(cancellationToken);
        private async Task<Customer> FindCustomer(int id, CancellationToken cancellationToken)
     => await _context.Customers.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
