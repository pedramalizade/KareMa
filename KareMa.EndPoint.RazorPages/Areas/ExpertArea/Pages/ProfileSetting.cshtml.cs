using Framework;
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public List<int> ServiceIds { get; set; }

        [BindProperty]
        public List<ServicesNameDto> ServicesNames { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ تولد نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^(\\d{4})/(\\d{2})/(\\d{2})$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd باشد.")]
        [Length(10, 10, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 10 کاراکتر باشد")]
        public string BirthDate { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProfileSetting OnGet called.");
            Console.WriteLine("ProfileSetting OnGet called.");
            var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value ?? "0");
            ExpertUpdate = await _expertAppServices.ExpertUpdateInfo(expertId, cancellationToken);
            ServicesNames = await _serviceAppServices.GetServicesName(cancellationToken);
            if (ExpertUpdate?.BirthDate != null)
            {
                var birthDate = ExpertUpdate.BirthDate;
                BirthDate = birthDate.ToPersianString("yyyy/MM/dd");
            }
            _logger.LogInformation("Current Gender on Get: {Gender}", ExpertUpdate?.Gender);
            Console.WriteLine($"Current Gender on Get: {ExpertUpdate?.Gender}");
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProfileSetting OnPostUpdateProfile started.");
            Console.WriteLine("ProfileSetting OnPostUpdateProfile started.");

            try
            {
                _logger.LogInformation("Received Data - ID: {Id}, Gender: {Gender}, PhoneNumber: {PhoneNumber}, BirthDate: '{BirthDate}'", ExpertUpdate.Id, ExpertUpdate.Gender, ExpertUpdate.PhoneNumber, BirthDate);
                Console.WriteLine($"Received Data - ID: {ExpertUpdate.Id}, Gender: {ExpertUpdate.Gender}, PhoneNumber: {ExpertUpdate.PhoneNumber}, BirthDate: '{BirthDate}'");

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
                    ServicesNames = await _serviceAppServices.GetServicesName(cancellationToken);
                    return Page();
                }

                var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value ?? "0");
                ExpertUpdate.Id = expertId;
                ExpertUpdate.ServiceIds = ServiceIds;

                if (!string.IsNullOrEmpty(BirthDate))
                {
                    try
                    {
                        if (!Regex.IsMatch(BirthDate, @"^\d{4}/\d{2}/\d{2}$"))
                        {
                            throw new FormatException($"فرمت تاریخ '{BirthDate}' اشتباه است؛ باید yyyy/MM/dd باشد (مثلاً 1368/10/10)");
                        }

                        var persianDateParts = BirthDate.Split('/');
                        if (persianDateParts.Length != 3)
                        {
                            throw new FormatException($"تاریخ '{BirthDate}' باید شامل سال، ماه و روز باشد");
                        }

                        var year = int.Parse(persianDateParts[0]);
                        var month = int.Parse(persianDateParts[1]);
                        var day = int.Parse(persianDateParts[2]);

                        var persianCalendar = new System.Globalization.PersianCalendar();
                        ExpertUpdate.BirthDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                        Console.WriteLine($"BirthDate parsed successfully: {ExpertUpdate.BirthDate}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Failed to parse BirthDate '{BirthDate}': {Message}", BirthDate, ex.Message);
                        Console.WriteLine($"Failed to parse BirthDate '{BirthDate}': {ex.Message}");
                        ModelState.AddModelError("BirthDate", $"خطا در تبدیل تاریخ تولد: {ex.Message}");
                        ServicesNames = await _serviceAppServices.GetServicesName(cancellationToken);
                        return Page();
                    }
                }
                else
                {
                    Console.WriteLine("BirthDate is empty or null.");
                }

                _logger.LogInformation("Updating expert with ID: {ExpertId}, Gender: {Gender}", expertId, ExpertUpdate.Gender);
                Console.WriteLine($"Updating expert with ID: {expertId}, Gender: {ExpertUpdate.Gender}");

                var result = await _expertAppServices.Update(ExpertUpdate, Image,  cancellationToken);
                if (!result)
                {
                    _logger.LogWarning("Failed to update expert profile.");
                    Console.WriteLine("Failed to update expert profile.");
                    ModelState.AddModelError("", "خطا در ذخیره تغییرات پروفایل");
                    ServicesNames = await _serviceAppServices.GetServicesName(cancellationToken);
                    return Page();
                }

                _logger.LogInformation("Expert profile updated successfully.");
                Console.WriteLine("Expert profile updated successfully.");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnPostUpdateProfile: {Message}", ex.Message);
                Console.WriteLine($"Error in OnPostUpdateProfile: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"خطا در ذخیره تغییرات: {ex.Message} - جزئیات: {ex.InnerException?.Message}");
                ServicesNames = await _serviceAppServices.GetServicesName(cancellationToken);
                return Page();
            }
        }
    }
}
