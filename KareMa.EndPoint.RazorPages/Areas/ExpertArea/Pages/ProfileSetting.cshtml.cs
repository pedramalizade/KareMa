namespace KareMa.EndPoint.RazorPages.Areas.ExpertArea.Pages
{

    [Authorize(Roles = "Expert")]
    public class ProfileSettingModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ILogger<ProfileSettingModel> _logger;

        public ProfileSettingModel(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices,
            ILogger<ProfileSettingModel> logger)
        {
            _expertAppServices = expertAppServices;
            _serviceAppServices = serviceAppServices;
            _logger = logger;
        }

        [BindProperty]
        public ExpertUpdateDto ExpertUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<ServicesNameDto> ServicesNames { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ تولد نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^(\\d{4})/(\\d{2})/(\\d{2})$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd باشد.")]
        [Length(10, 10, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 10 کاراکتر باشد")]
        public string BirthDate { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
            if (!int.TryParse(expertIdClaim, out var expertId))
            {
                throw new UnauthorizedAccessException("شناسه کارشناس یافت نشد.");
            }

            ExpertUpdate = await _expertAppServices.ExpertUpdateInfoAsync(expertId, cancellationToken);
            if (ExpertUpdate == null)
            {
                ExpertUpdate = new ExpertUpdateDto { Id = expertId };
            }

            ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
            BirthDate = ExpertUpdate.BirthDate != null
                ? ExpertUpdate.BirthDate.ToPersianString("yyyy/MM/dd")
                : string.Empty;

            _logger.LogInformation("شناسه سرویس‌ها در GET: {@ServiceIds}", ExpertUpdate?.ServiceIds ?? new List<int>());
            _logger.LogInformation("جنسیت در GET: {Gender}", ExpertUpdate?.Gender);
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(CancellationToken cancellationToken)
        {
            try
            {
                LogReceivedData();
                if (!ModelState.IsValid)
                {
                    AddModelStateErrors();
                    return await ReturnWithServices(cancellationToken);
                }

                var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value ?? "0");
                ExpertUpdate.Id = expertId;

                if (!string.IsNullOrEmpty(BirthDate) && !await TryParseBirthDateAsync(cancellationToken))
                {
                    return await ReturnWithServices(cancellationToken);
                }

                if (Image != null && !await TryUploadImageAsync(cancellationToken))
                {
                    return await ReturnWithServices(cancellationToken);
                }

                var result = await _expertAppServices.UpdateAsync(ExpertUpdate, Image, cancellationToken);
                if (!result)
                {
                    _logger.LogWarning("ذخیره تغییرات پروفایل کارشناس ناموفق بود.");
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    return await ReturnWithServices(cancellationToken);
                }

                _logger.LogInformation("پروفایل کارشناس با موفقیت به‌روزرسانی شد.");
                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در به‌روزرسانی پروفایل: {Message}", ex.Message);
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message ?? "جزئیات بیشتری در دسترس نیست"}");
                return await ReturnWithServices(cancellationToken);
            }
        }

        private void LogReceivedData()
        {
            _logger.LogInformation("اطلاعات دریافتی - شناسه: {Id}, جنسیت: {Gender}, تلفن: {PhoneNumber}, تاریخ تولد: '{BirthDate}', سرویس‌ها: {@ServiceIds}",
                ExpertUpdate.Id, ExpertUpdate.Gender, ExpertUpdate.PhoneNumber, BirthDate, ExpertUpdate.ServiceIds ?? new List<int>());
        }

        private void AddModelStateErrors()
        {
            _logger.LogWarning("مدل ورودی معتبر نیست.");
            foreach (var (key, value) in ModelState)
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    ModelState.AddModelError("", $"خطا در {key}: {error.ErrorMessage}");
                }
        }

        private async Task<IActionResult> ReturnWithServices(CancellationToken cancellationToken)
        {
            ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
            return Page();
        }

        private async Task<bool> TryParseBirthDateAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (!Regex.IsMatch(BirthDate, @"^\d{4}/\d{2}/\d{2}$"))
                    throw new FormatException($"فرمت تاریخ '{BirthDate}' اشتباه است؛ باید yyyy/MM/dd باشد (مثلاً 1368/10/10)");

                var parts = BirthDate.Split('/');
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);

                var persianCalendar = new PersianCalendar();
                ExpertUpdate.BirthDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("تبدیل تاریخ تولد '{BirthDate}' ناموفق بود: {Message}", BirthDate, ex.Message);
                ModelState.AddModelError("BirthDate", $"خطا در تبدیل تاریخ تولد: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TryUploadImageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using var stream = new FileStream(filePath, FileMode.Create);
                await Image.CopyToAsync(stream, cancellationToken);

                ExpertUpdate.Image = $"/uploads/{fileName}";
                _logger.LogInformation("تصویر با موفقیت آپلود شد: {ImagePath}", ExpertUpdate.Image);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("آپلود تصویر ناموفق بود: {Message}", ex.Message);
                ModelState.AddModelError("", $"خطا در آپلود تصویر: {ex.Message}");
                return false;
            }
        }
    }
}
