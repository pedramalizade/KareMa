namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICustomerAppServices
    {
        Task<bool> CreateAsync(CustomerCreateDto customerCreateDto,IFormFile Image, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, IFormFile Image, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken);
        Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<int> CustomerCountAsync(CancellationToken cancellationToken);
        Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken);
        Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken);
        Task<CustomerUpdateDto> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<OperationResult> UpdateProfileAsync(int userCustomerId, CustomerUpdateDto customerUpdateDto, IFormFile? image, CancellationToken cancellationToken);


    }
}
