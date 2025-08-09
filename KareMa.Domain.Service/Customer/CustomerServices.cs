namespace KareMa.Domain.Service
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
          => _customerRepository.CreateAsync(customerCreateDto, cancellationToken);
        public Task<int> CustomerCountAsync(CancellationToken cancellationToken)
          => _customerRepository.CustomerCountAsync(cancellationToken);
        public Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
          => _customerRepository.DeleteAsync(customerId, cancellationToken);
        public Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken)
          => _customerRepository.GetAllAsync(cancellationToken);
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken)
   => await _customerRepository.GetCustomerUpdateInfoAsync(customerId, cancellationToken);
        public Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken)
          => _customerRepository.GetByIdAsync(customerId, cancellationToken);
        public async Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken)
       => await _customerRepository.GetCustomerSummaryAsync(id, cancellationToken);
        public async Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
          => await _customerRepository.UpdateAsync(customerUpdateDto, cancellationToken);
        public async Task<CustomerUpdateDto> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken)
        => await _customerRepository.CustomerUpdateInfoAsync(id,  cancellationToken);
        public async Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
        => await _customerRepository.GetCustomerByIdAsync(customerId, cancellationToken);
        public async Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken)
       => await _customerRepository.UpdateBalanceAsync(customerId, newBalance, cancellationToken);   
    }
}
