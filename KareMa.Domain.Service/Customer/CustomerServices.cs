namespace KareMa.Domain.Service
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        /// <summary>ایجاد مشتری جدید</summary>
        public Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
            => _customerRepository.CreateAsync(customerCreateDto, cancellationToken);

        /// <summary>تعداد مشتری‌ها</summary>
        public Task<int> CustomerCountAsync(CancellationToken cancellationToken)
            => _customerRepository.CustomerCountAsync(cancellationToken);

        /// <summary>حذف مشتری</summary>
        public Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
            => _customerRepository.DeleteAsync(customerId, cancellationToken);

        /// <summary>دریافت همه مشتری‌ها</summary>
        public Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken)
            => _customerRepository.GetAllAsync(cancellationToken);

        /// <summary>اطلاعات ویرایش مشتری</summary>
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken)
            => await _customerRepository.GetCustomerUpdateInfoAsync(customerId, cancellationToken);

        /// <summary>دریافت مشتری با شناسه</summary>
        public Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken)
            => _customerRepository.GetByIdAsync(customerId, cancellationToken);

        /// <summary>خلاصه اطلاعات مشتری</summary>
        public async Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken)
            => await _customerRepository.GetCustomerSummaryAsync(id, cancellationToken);

        /// <summary>ویرایش مشتری</summary>
        public async Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
            => await _customerRepository.UpdateAsync(customerUpdateDto, cancellationToken);

        /// <summary>دریافت اطلاعات جهت ویرایش</summary>
        public async Task<CustomerUpdateDto> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken)
            => await _customerRepository.CustomerUpdateInfoAsync(id, cancellationToken);

        /// <summary>دریافت مشتری با شناسه</summary>
        public async Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
            => await _customerRepository.GetCustomerByIdAsync(customerId, cancellationToken);

        /// <summary>به‌روزرسانی موجودی مشتری</summary>
        public async Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken)
            => await _customerRepository.UpdateBalanceAsync(customerId, newBalance, cancellationToken);
    }
}
