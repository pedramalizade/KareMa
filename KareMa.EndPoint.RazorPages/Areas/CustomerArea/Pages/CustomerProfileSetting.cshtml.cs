
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages.Shared
{
    [Authorize(Roles = "Customer")]
    public class CustomerProfileSettingModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;
        private readonly ILogger<CustomerProfileSettingModel> _logger;

        public CustomerProfileSettingModel(
            ICustomerAppServices customerAppServices,
            ICityAppServices cityAppService,
            ILogger<CustomerProfileSettingModel> logger)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
            _logger = logger;
        }

        [BindProperty]
        public CustomerUpdateDto CustomerUpdate { get; set; } = new CustomerUpdateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<City> Cities { get; set; } = new List<City>();

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CustomerProfileSetting OnGet started.");
            Console.WriteLine("CustomerProfileSetting OnGet started.");

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                _logger.LogWarning("No valid customer ID found in claims.");
                Console.WriteLine("No valid customer ID found in claims.");
                return Unauthorized();
            }

            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfo(userCustomerId, cancellationToken);
            if (CustomerUpdate == null)
            {
                _logger.LogWarning("Customer with ID {UserCustomerId} not found.", userCustomerId);
                Console.WriteLine($"Customer with ID {userCustomerId} not found.");
                return NotFound();
            }

            Cities = await _cityAppService.GetAll(cancellationToken);
            _logger.LogInformation("Customer data loaded successfully for ID: {UserCustomerId}", userCustomerId);
            Console.WriteLine($"Customer data loaded successfully for ID: {userCustomerId}");

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CustomerProfileSetting OnPostUpdate started.");
            Console.WriteLine("CustomerProfileSetting OnPostUpdate started.");

            try
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
                if (userCustomerId == 0)
                {
                    _logger.LogWarning("No valid customer ID found in claims.");
                    Console.WriteLine("No valid customer ID found in claims.");
                    ModelState.AddModelError("", "کاربر معتبر نیست.");
                    Cities = await _cityAppService.GetAll(cancellationToken);
                    return Page();
                }

                _logger.LogInformation("Received Data - ID: {Id}, Gender: {Gender}, PhoneNumber: {PhoneNumber}, Address.Title: {Title}, Address.CityId: {CityId}",
                    userCustomerId, CustomerUpdate.Gender, CustomerUpdate.PhoneNumber, CustomerUpdate.Address?.Title, CustomerUpdate.Address?.CityId);
                Console.WriteLine($"Received Data - ID: {userCustomerId}, Gender: {CustomerUpdate.Gender}, PhoneNumber: {CustomerUpdate.PhoneNumber}, Address.Title: {CustomerUpdate.Address?.Title}, Address.CityId: {CustomerUpdate.Address?.CityId}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid.");
                    Console.WriteLine("ModelState is invalid.");
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var value = ModelState[modelStateKey];
                        foreach (var error in value.Errors)
                        {
                            Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                            ModelState.AddModelError("", $"خطا در {modelStateKey}: {error.ErrorMessage}");
                        }
                    }
                    Cities = await _cityAppService.GetAll(cancellationToken);
                    return Page();
                }

                CustomerUpdate.Id = userCustomerId;
                if (CustomerUpdate.Address != null)
                {
                    // اگه آدرس از قبل وجود داره، فقط به‌روزش می‌کنیم
                    CustomerUpdate.Address.CustomerId = userCustomerId;
                    if (string.IsNullOrEmpty(CustomerUpdate.Address.Title))
                    {
                        CustomerUpdate.Address.Title = "آدرس پیش‌فرض";
                    }
                }
                else
                {
                    CustomerUpdate.Address = new Address { CustomerId = userCustomerId, Title = "آدرس پیش‌فرض" };
                }

                if (Image != null)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream, cancellationToken);
                        }
                        CustomerUpdate.Image = $"/uploads/{fileName}";
                        _logger.LogInformation("Image uploaded successfully: {ImagePath}", CustomerUpdate.Image);
                        Console.WriteLine($"Image uploaded successfully: {CustomerUpdate.Image}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to upload image: {Message}", ex.Message);
                        Console.WriteLine($"Failed to upload image: {ex.Message}");
                        ModelState.AddModelError("", $"خطا در آپلود تصویر: {ex.Message}");
                        Cities = await _cityAppService.GetAll(cancellationToken);
                        return Page();
                    }
                }

                _logger.LogInformation("Updating customer with ID: {CustomerId}, Gender: {Gender}", userCustomerId, CustomerUpdate.Gender);
                Console.WriteLine($"Updating customer with ID: {userCustomerId}, Gender: {CustomerUpdate.Gender}");

                var result = await _customerAppServices.Update(CustomerUpdate, Image, cancellationToken);
                if (!result)
                {
                    _logger.LogWarning("Failed to update customer profile.");
                    Console.WriteLine("Failed to update customer profile.");
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    Cities = await _cityAppService.GetAll(cancellationToken);
                    return Page();
                }

                _logger.LogInformation("Customer profile updated successfully.");
                Console.WriteLine("Customer profile updated successfully.");
                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnPostUpdate: {Message}", ex.Message);
                Console.WriteLine($"Error in OnPostUpdate: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message ?? "جزئیات بیشتری در دسترس نیست"}");
                Cities = await _cityAppService.GetAll(cancellationToken);
                return Page();
            }
        }
    }
}

//namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages.Shared
//{
//    [Authorize(Roles = "Customer")]
//    public class CustomerProfileSettingModel : PageModel
//    {
//        private readonly ICustomerAppServices _customerAppServices;
//        private readonly ICityAppServices _cityAppService;

//        public CustomerProfileSettingModel(ICustomerAppServices customerAppServices, ICityAppServices cityAppService)
//        {
//            _customerAppServices = customerAppServices;
//            _cityAppService = cityAppService;
//        }

//        [BindProperty]
//        public CustomerUpdateDto CustomerUpdate { get; set; } = new CustomerUpdateDto();

//        [BindProperty]
//        public List<City> Cities { get; set; } = new List<City>();

//        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
//        {
//            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
//            if (userCustomerId == 0)
//            {
//                return Unauthorized();
//            }

//            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfo(userCustomerId, cancellationToken);
//            Cities = await _cityAppService.GetAll(cancellationToken);

//            return Page();
//        }

//        public async Task<IActionResult> OnPost(CustomerUpdateDto customerUpdate, IFormFile Image, CancellationToken cancellationToken)
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
//            if (userCustomerId == 0)
//            {
//                ModelState.AddModelError("", "???? ????? ????");
//                return Page();
//            }

//            customerUpdate.Id = userCustomerId;

//            if (customerUpdate.Address == null)
//            {
//                customerUpdate.Address = new Address();
//            }
//            customerUpdate.Address.CustomerId = userCustomerId;

//            if (Image != null)
//            {
//                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
//                var filePath = Path.Combine("wwwroot/uploads", fileName);

//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await Image.CopyToAsync(stream, cancellationToken);
//                }

//                customerUpdate.Image = $"/uploads/{fileName}";
//            }

//            var result = await _customerAppServices.Update(customerUpdate, Image, cancellationToken);
//            if (!result)
//            {
//                ModelState.AddModelError("", "????? ?? ????? ??????? ??? ???.");
//                return Page();
//            }

//            TempData["SuccessMessage"] = "??????? ?? ?????? ????? ??.";
//            return RedirectToPage();
//        }
//    }
//}

