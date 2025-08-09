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
                _logger.LogError("Could not retrieve or parse expertId from claims.");
                throw new UnauthorizedAccessException("شناسه کارشناس یافت نشد.");
            }

            ExpertUpdate = await _expertAppServices.ExpertUpdateInfoAsync(expertId, cancellationToken);
            if (ExpertUpdate == null)
            {
                _logger.LogWarning("Expert with ID {ExpertId} not found, initializing empty DTO.", expertId);
                ExpertUpdate = new ExpertUpdateDto { Id = expertId };
            }

            ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
            BirthDate = ExpertUpdate.BirthDate != null ? ExpertUpdate.BirthDate.ToPersianString("yyyy/MM/dd") : string.Empty;

            _logger.LogInformation("ServiceIds on Get: {@ServiceIds}", ExpertUpdate?.ServiceIds ?? new List<int>());
            _logger.LogInformation("Gender on Get: {Gender}", ExpertUpdate?.Gender);
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProfileSetting OnPostUpdateProfile started.");
            Console.WriteLine("ProfileSetting OnPostUpdateProfile started.");

            try
            {
                _logger.LogInformation("Received Data - ID: {Id}, Gender: {Gender}, PhoneNumber: {PhoneNumber}, BirthDate: '{BirthDate}', ServiceIds: {@ServiceIds}",
                    ExpertUpdate.Id, ExpertUpdate.Gender, ExpertUpdate.PhoneNumber, BirthDate, ExpertUpdate.ServiceIds ?? new List<int>());
                Console.WriteLine($"Received Data - ID: {ExpertUpdate.Id}, Gender: {ExpertUpdate.Gender}, PhoneNumber: {ExpertUpdate.PhoneNumber}, BirthDate: '{BirthDate}', ServiceIds: {string.Join(", ", ExpertUpdate.ServiceIds ?? new List<int>())}");

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
                    ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                    return Page();
                }

                var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value ?? "0");
                ExpertUpdate.Id = expertId;

                if (!string.IsNullOrEmpty(BirthDate))
                {
                    try
                    {
                        if (!Regex.IsMatch(BirthDate, @"^\d{4}/\d{2}/\d{2}$"))
                        {
                            throw new FormatException($"فرمت تاریخ '{BirthDate}' اشتباه است؛ باید yyyy/MM/dd باشد (مثلاً 1368/10/10)");
                        }

                        var persianDateParts = BirthDate.Split('/');
                        var year = int.Parse(persianDateParts[0]);
                        var month = int.Parse(persianDateParts[1]);
                        var day = int.Parse(persianDateParts[2]);

                        var persianCalendar = new PersianCalendar();
                        ExpertUpdate.BirthDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                        Console.WriteLine($"BirthDate parsed successfully: {ExpertUpdate.BirthDate}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Failed to parse BirthDate '{BirthDate}': {Message}", BirthDate, ex.Message);
                        Console.WriteLine($"Failed to parse BirthDate '{BirthDate}': {ex.Message}");
                        ModelState.AddModelError("BirthDate", $"خطا در تبدیل تاریخ تولد: {ex.Message}");
                        ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                        return Page();
                    }
                }

                // آپلود تصویر اگه وجود داره
                if (Image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    ExpertUpdate.Image = $"/uploads/{fileName}";
                    _logger.LogInformation("Image uploaded: {ImagePath}", ExpertUpdate.Image);
                }

                _logger.LogInformation("Updating expert with ID: {ExpertId}, ServiceIds: {@ServiceIds}", expertId, ExpertUpdate.ServiceIds);
                Console.WriteLine($"Updating expert with ID: {expertId}, ServiceIds: {string.Join(", ", ExpertUpdate.ServiceIds ?? new List<int>())}");

                var result = await _expertAppServices.UpdateAsync(ExpertUpdate,Image,  cancellationToken); // Image رو اینجا نمی‌فرستیم چون توی DTO هست
                if (!result)
                {
                    _logger.LogWarning("Failed to update expert profile.");
                    Console.WriteLine("Failed to update expert profile.");
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                    return Page();
                }

                _logger.LogInformation("Expert profile updated successfully.");
                Console.WriteLine("Expert profile updated successfully.");
                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnPostUpdateProfile: {Message}", ex.Message);
                Console.WriteLine($"Error in OnPostUpdateProfile: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message}");
                ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                return Page();
            }
        }
    }
}
