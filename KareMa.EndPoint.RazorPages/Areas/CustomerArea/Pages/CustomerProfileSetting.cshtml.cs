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
                    return await ReturnWithError("کاربر معتبر نیست.", cancellationToken);
                }

                if (!ModelState.IsValid)
                {
                    AddModelStateErrors();
                    return await ReturnWithCities(cancellationToken);
                }

                PrepareCustomerUpdate(userCustomerId);

                if (Image != null && !await TryUploadImageAsync(cancellationToken))
                {
                    return await ReturnWithCities(cancellationToken);
                }

                var result = await _customerAppServices.UpdateAsync(CustomerUpdate, Image, cancellationToken);
                if (!result)
                {
                    return await ReturnWithError("خطا در ذخیره تغییرات پروفایل", cancellationToken);
                }

                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                var errorMessage = $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message ?? "جزئیات بیشتری در دسترس نیست"}";
                return await ReturnWithError(errorMessage, cancellationToken);
            }
        }

        private void AddModelStateErrors()
        {
            foreach (var (key, value) in ModelState)
                foreach (var error in value.Errors)
                    ModelState.AddModelError("", $"خطا در {key}: {error.ErrorMessage}");
        }

        private void PrepareCustomerUpdate(int userCustomerId)
        {
            CustomerUpdate.Id = userCustomerId;

            if (CustomerUpdate.Address == null)
            {
                CustomerUpdate.Address = new Address { CustomerId = userCustomerId, Title = "آدرس پیش‌فرض" };
            }
            else
            {
                CustomerUpdate.Address.CustomerId = userCustomerId;
                CustomerUpdate.Address.Title ??= "آدرس پیش‌فرض";
            }
        }

        private async Task<bool> TryUploadImageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using var stream = new FileStream(filePath, FileMode.Create);
                await Image.CopyToAsync(stream, cancellationToken);

                CustomerUpdate.Image = $"/uploads/{fileName}";
                return true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطا در آپلود تصویر: {ex.Message}");
                return false;
            }
        }

        private async Task<IActionResult> ReturnWithError(string message, CancellationToken cancellationToken)
        {
            ModelState.AddModelError("", message);
            return await ReturnWithCities(cancellationToken);
        }

        private async Task<IActionResult> ReturnWithCities(CancellationToken cancellationToken)
        {
            Cities = await _cityAppService.GetAllAsync(cancellationToken);
            return Page();
        }
    }
}