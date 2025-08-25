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

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                return Unauthorized();
            }

            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfoAsync(userCustomerId, cancellationToken);
            if (CustomerUpdate == null)
            {
                return NotFound();
            }

            Cities = await _cityAppService.GetAllAsync(cancellationToken);

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {

            try
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
                if (userCustomerId == 0)
                {
                    ModelState.AddModelError("", "کاربر معتبر نیست.");
                    Cities = await _cityAppService.GetAllAsync(cancellationToken);
                    return Page();
                }


                if (!ModelState.IsValid)
                {
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var value = ModelState[modelStateKey];
                        foreach (var error in value.Errors)
                        {
                            ModelState.AddModelError("", $"خطا در {modelStateKey}: {error.ErrorMessage}");
                        }
                    }
                    Cities = await _cityAppService.GetAllAsync(cancellationToken);
                    return Page();
                }

                CustomerUpdate.Id = userCustomerId;
                if (CustomerUpdate.Address != null)
                {
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
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"خطا در آپلود تصویر: {ex.Message}");
                        Cities = await _cityAppService.GetAllAsync(cancellationToken);
                        return Page();
                    }
                }


                var result = await _customerAppServices.UpdateAsync(CustomerUpdate, Image, cancellationToken);
                if (!result)
                {
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    Cities = await _cityAppService.GetAllAsync(cancellationToken);
                    return Page();
                }

                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message ?? "جزئیات بیشتری در دسترس نیست"}");
                Cities = await _cityAppService.GetAllAsync(cancellationToken);
                return Page();
            }
        }
    }
}