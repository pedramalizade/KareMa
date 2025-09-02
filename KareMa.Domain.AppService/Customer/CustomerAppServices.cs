namespace KareMa.Domain.AppService
{
    public class CustomerAppServices : ICustomerAppServices
    {
        private readonly ICustomerServices _customerServices;
        private readonly IBaseSevices _baseSevices;
        public CustomerAppServices(ICustomerServices customerServices, IBaseSevices baseSevices)
        {
            _customerServices = customerServices;
            _baseSevices = baseSevices;
        }
        public async Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, IFormFile image, CancellationToken cancellationToken)
        {
            var imageAddress = await _baseSevices.UploadImage(image);

            if (string.IsNullOrEmpty(imageAddress))
            {
                Console.WriteLine("Image upload failed!");
                return false;
            }

            customerCreateDto.Image = imageAddress;
            return await _customerServices.CreateAsync(customerCreateDto, cancellationToken);
        }
        public async Task<OperationResult> UpdateProfileAsync(int userCustomerId, CustomerUpdateDto customerUpdateDto, IFormFile? image, CancellationToken cancellationToken)
        {
            try
            {
                PrepareCustomerUpdate(userCustomerId, customerUpdateDto);

                if (image != null)
                {
                    var imageUrl = await _baseSevices.UploadImage(image);
                    if (string.IsNullOrEmpty(imageUrl))
                        return OperationResult.Fail("آپلود تصویر ناموفق بود.");
                    customerUpdateDto.Image = imageUrl;
                }

                var result = await _customerServices.UpdateAsync(customerUpdateDto, cancellationToken);
                return result
                    ? OperationResult.SuccessResult()
                    : OperationResult.Fail("ذخیره تغییرات پروفایل ناموفق بود.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"خطا در ذخیره تغییرات: {ex.Message}");
            }
        }

        private void PrepareCustomerUpdate(int userCustomerId, CustomerUpdateDto dto)
        {
            dto.Id = userCustomerId;
            if (dto.Address == null)
            {
                dto.Address = new Address { CustomerId = userCustomerId, Title = "آدرس پیش‌فرض" };
            }
            else
            {
                dto.Address.CustomerId = userCustomerId;
                dto.Address.Title ??= "آدرس پیش‌فرض";
            }
        }
       

        public async Task<int> CustomerCountAsync(CancellationToken cancellationToken)
          => await _customerServices.CustomerCountAsync(cancellationToken);
        public async Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken)
          => await _customerServices.GetCustomerSummaryAsync(id, cancellationToken);
        public async Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
          => await _customerServices.DeleteAsync(customerId, cancellationToken);
        public async Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken)
          => await _customerServices.GetAllAsync(cancellationToken);
        public async Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken)
          => await _customerServices.GetByIdAsync(customerId, cancellationToken);
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken)
  => await _customerServices.GetCustomerUpdateInfoAsync(customerId, cancellationToken);
        public async Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, IFormFile Image, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CustomerAppServices.Update started for ID: {customerUpdateDto.Id}");

            if (Image != null)
            {
                try
                {
                    var imageAddress = await _baseSevices.UploadImage(Image);
                    customerUpdateDto.Image = imageAddress;
                    Console.WriteLine($"Image uploaded: {customerUpdateDto.Image}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Image upload failed: {ex.Message}");
                    return false; 
                }
            }

            var result = await _customerServices.UpdateAsync(customerUpdateDto, cancellationToken);
            Console.WriteLine($"CustomerServices.Update result: {result}");
            return result;
        }
        public async Task<CustomerUpdateDto> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken)
      => await _customerServices.CustomerUpdateInfoAsync(id, cancellationToken);
        public async Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
       => await _customerServices.GetCustomerByIdAsync(customerId, cancellationToken);
        public async Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken)
      => await _customerServices.UpdateBalanceAsync(customerId, newBalance, cancellationToken);  
    }
}
