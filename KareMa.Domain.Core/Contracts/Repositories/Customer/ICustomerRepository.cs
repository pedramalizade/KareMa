namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> Create(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int customerId, CancellationToken cancellationToken);
        Task<Customer> GetById(int customerId, CancellationToken cancellationToken);
        Task<List<GetCustomerDto>> GetAll(CancellationToken cancellationToken);
        Task<int> CustomerCount(CancellationToken cancellationToken);
        Task UpdateBalance(int customerId, decimal newBalance, CancellationToken cancellationToken);
        Task<Customer> GetCustomerById(int customerId, CancellationToken cancellationToken);
        Task<CustomerUpdateDto> GetCustomerUpdateInfo(int customerId, CancellationToken cancellationToken);
        Task<CustomerUpdateDto> CustomerUpdateInfo(int id, CancellationToken cancellationToken);
        Task<CustomerSummaryDto> GetCustomerSummary(int id, CancellationToken cancellationToken);
    }
}
