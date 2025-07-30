namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages.Shared
{
    [Authorize(Roles = "Customer")]
    public class CustomerProfileSettingModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;

        public CustomerProfileSettingModel(
            ICustomerAppServices customerAppServices,
            ICityAppServices cityAppService)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
        }

        [BindProperty]
        public CustomerUpdateDto CustomerUpdate { get; set; } = new CustomerUpdateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<City> Cities { get; set; } = new List<City>();

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("CustomerProfileSetting OnGet started.");

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                Console.WriteLine("No valid customer ID found in claims.");
                return Unauthorized();
            }

            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfo(userCustomerId, cancellationToken);
            if (CustomerUpdate == null)
            {
                Console.WriteLine($"Customer with ID {userCustomerId} not found.");
                return NotFound();
            }

            Cities = await _cityAppService.GetAll(cancellationToken);
            Console.WriteLine($"Customer data loaded successfully for ID: {userCustomerId}");

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("CustomerProfileSetting OnPostUpdate started.");

            try
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
                if (userCustomerId == 0)
                {
                    Console.WriteLine("No valid customer ID found in claims.");
                    ModelState.AddModelError("", "کاربر معتبر نیست.");
                    Cities = await _cityAppService.GetAll(cancellationToken);
                    return Page();
                }

                Console.WriteLine($"Received Data - ID: {userCustomerId}, Gender: {CustomerUpdate.Gender}, PhoneNumber: {CustomerUpdate.PhoneNumber}, Address.Title: {CustomerUpdate.Address?.Title}, Address.CityId: {CustomerUpdate.Address?.CityId}");

                if (!ModelState.IsValid)
                {
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
                        Console.WriteLine($"Image uploaded successfully: {CustomerUpdate.Image}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to upload image: {ex.Message}");
                        ModelState.AddModelError("", $"خطا در آپلود تصویر: {ex.Message}");
                        Cities = await _cityAppService.GetAll(cancellationToken);
                        return Page();
                    }
                }

                Console.WriteLine($"Updating customer with ID: {userCustomerId}, Gender: {CustomerUpdate.Gender}");

                var result = await _customerAppServices.Update(CustomerUpdate, Image, cancellationToken);
                if (!result)
                {
                    Console.WriteLine("Failed to update customer profile.");
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    Cities = await _cityAppService.GetAll(cancellationToken);
                    return Page();
                }

                Console.WriteLine("Customer profile updated successfully.");
                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostUpdate: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message ?? "جزئیات بیشتری در دسترس نیست"}");
                Cities = await _cityAppService.GetAll(cancellationToken);
                return Page();
            }
        }
    }
}