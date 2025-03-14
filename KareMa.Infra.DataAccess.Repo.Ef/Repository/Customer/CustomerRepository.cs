using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                BankCardNumber = customerCreateDto.BankCardNumber,
                Addresses = customerCreateDto.Addresses,
                Image = customerCreateDto.Image,
                AppUserId = customerCreateDto.AppUserId,
                Balance = customerCreateDto.Balance ?? 0
            };
            await _context.Customers.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int customerId, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Attempting to delete customer with ID: {customerId}");
            var targetModel = await FindCustomer(customerId, cancellationToken);
            if (targetModel == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found.");
                return false;
            }

            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken); 
            Console.WriteLine($"Customer with ID: {customerId} marked as deleted.");
            return true;
        }

        public async Task<List<GetCustomerDto>> GetAll(CancellationToken cancellationToken)
        {
            Console.WriteLine("Fetching all customers...");
            var customers = await _context.Customers
                .AsNoTracking()
                .Where(c => !c.IsDeleted) 
                .Select(c => new GetCustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Image = c.Image,
                    Balance = c.Balance
                }).ToListAsync(cancellationToken);

            Console.WriteLine($"Found {customers.Count} active customers.");
            return customers;
        }

        public async Task<Customer> GetById(int customerId, CancellationToken cancellationToken)
        {
            return await FindCustomer(customerId, cancellationToken);
        }

        public async Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CustomerRepository.Update started for ID: {customerUpdateDto.Id}");

            var targetModel = await _context.Customers
                .Include(c => c.Addresses) 
                .FirstOrDefaultAsync(c => c.Id == customerUpdateDto.Id && !c.IsDeleted, cancellationToken);

            if (targetModel == null)
            {
                Console.WriteLine($"Customer with ID {customerUpdateDto.Id} not found.");
                return false;
            }

            targetModel.FirstName = customerUpdateDto.FirstName;
            targetModel.LastName = customerUpdateDto.LastName;
            targetModel.Balance = customerUpdateDto.Balance;
            targetModel.Image = customerUpdateDto.Image;
            targetModel.BankCardNumber = customerUpdateDto.BankCardNumber;
            targetModel.PhoneNumber = customerUpdateDto.PhoneNumber;
            targetModel.Gender = customerUpdateDto.Gender;

            if (customerUpdateDto.Address != null)
            {
                if (targetModel.Addresses != null)
                {
                    targetModel.Addresses.Title = customerUpdateDto.Address.Title;
                    targetModel.Addresses.CityId = customerUpdateDto.Address.CityId;
                    targetModel.Addresses.Street = customerUpdateDto.Address.Street;
                    targetModel.Addresses.Area = customerUpdateDto.Address.Area;
                    targetModel.Addresses.PostalCode = customerUpdateDto.Address.PostalCode;
                    Console.WriteLine($"Updated existing address for Customer ID: {customerUpdateDto.Id}");
                }
                else
                {
                    targetModel.Addresses = customerUpdateDto.Address;
                    Console.WriteLine($"Added new address for Customer ID: {customerUpdateDto.Id}");
                }
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"Customer with ID {customerUpdateDto.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
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
                    Balance = c.Balance,
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
            Console.WriteLine($"GetCustomerUpdateInfo called with customerId: {customerId}");

            var targetCustomer = await _context.Customers
                .AsNoTracking()
                .Include(c => c.Addresses)
                .Where(c => c.Id == customerId && !c.IsDeleted) 
                .Select(c => new CustomerUpdateDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Addresses,
                    Balance = c.Balance, 
                    BankCardNumber = c.BankCardNumber,
                    Gender = c.Gender,
                    Image = c.Image
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (targetCustomer == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found in database.");
                return null;
            }

            Console.WriteLine($"Found customer with ID: {targetCustomer.Id}, Name: {targetCustomer.FirstName} {targetCustomer.LastName}");
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

        public async Task<CustomerUpdateDto> CustomerUpdateInfo(int id, CancellationToken cancellationToken)
        {
            return await _context.Customers.Select(a => new CustomerUpdateDto
            {
                Id = id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Address = a.Addresses,
                Image = a.Image,
                Gender = a.Gender,
                Balance = a.Balance,
                PhoneNumber = a.PhoneNumber

            }).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task UpdateBalance(int customerId, decimal newBalance, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Updating balance for Customer ID: {customerId} to {newBalance}");
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerId && !c.IsDeleted, cancellationToken);

            if (customer == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found or is deleted.");
                throw new Exception($"Customer with ID {customerId} not found.");
            }

            customer.Balance = newBalance;
            await _context.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"Balance updated successfully for Customer ID: {customerId}");
        }
        public async Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Fetching customer with ID: {customerId}");
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerId && !c.IsDeleted, cancellationToken);

            if (customer == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found or is deleted.");
            }
            return customer;
        }
    }
}
