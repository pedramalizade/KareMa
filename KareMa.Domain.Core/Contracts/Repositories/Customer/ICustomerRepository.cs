namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken);
        Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CustomerCountAsync(CancellationToken cancellationToken);
        Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken);
        Task<CustomerUpdateDto?> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken);
    }
}
