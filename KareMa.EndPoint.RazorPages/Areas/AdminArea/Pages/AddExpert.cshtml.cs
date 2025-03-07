using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddExpertModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ICityAppServices _cityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AddExpertModel> _logger;

        public AddExpertModel(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices,
            ICityAppServices cityService,
            UserManager<AppUser> userManager,
            ILogger<AddExpertModel> logger)
        {
            _expertAppServices = expertAppServices ?? throw new ArgumentNullException(nameof(expertAppServices));
            _serviceAppServices = serviceAppServices ?? throw new ArgumentNullException(nameof(serviceAppServices));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public ExpertCreateDto ExpertCreate { get; set; } = new ExpertCreateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<Service> AllServices { get; set; }

        public SelectList Cities { get; set; }
        public SelectList AvailableUsers { get; set; } // لیست کاربران موجود

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AddExpert OnGet called.");
            Console.WriteLine("AddExpert OnGet called.");
            ExpertCreate.Address = new Address();
            AllServices = await _serviceAppServices.GetAllServicesAsync(cancellationToken);
            var cities = await _cityService.GetAll(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");

            // کاربرانی که هنوز متخصص نیستن
            var usedAppUserIds = await _expertAppServices.GetAll(cancellationToken)
                .ContinueWith(t => t.Result.Select(e => e.AppUserId).ToList(), cancellationToken);
            var availableUsers = await _userManager.Users
                .Where(u => !usedAppUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync(cancellationToken);
            AvailableUsers = new SelectList(availableUsers, "Id", "UserName");
        }

        public async Task<IActionResult> OnPostAddCategoryAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AddExpert OnPostAddCategory started.");
            Console.WriteLine("AddExpert OnPostAddCategory started.");

            try
            {
                _logger.LogInformation("Checking ModelState...");
                Console.WriteLine("Checking ModelState...");

                ModelState.Remove("ExpertCreate.Image");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid.");
                    Console.WriteLine("ModelState is invalid.");
                    var errorMessages = ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .SelectMany(ms => ms.Value.Errors.Select(e => $"Key: {ms.Key}, Error: {e.ErrorMessage}"))
                        .ToList();
                    foreach (var error in errorMessages)
                    {
                        _logger.LogWarning(error);
                        Console.WriteLine(error);
                    }
                    ModelState.AddModelError("", "لطفاً خطاهای زیر را بررسی کنید: " + string.Join(" | ", errorMessages));
                    await LoadFormData(cancellationToken);
                    return Page();
                }

                // اینجا دیگه AppUserId رو از کاربر لاگین‌شده نمی‌گیریم، بلکه از فرم میاد
                var result = await _expertAppServices.Create(ExpertCreate, Image, cancellationToken);
                if (!result)
                {
                    _logger.LogWarning("Expert creation failed.");
                    Console.WriteLine("Expert creation failed.");
                    ModelState.AddModelError("", "خطا در ثبت متخصص: مشکل در آپلود عکس یا ذخیره اطلاعات");
                    await LoadFormData(cancellationToken);
                    return Page();
                }

                _logger.LogInformation("Expert created successfully. Redirecting to Experts...");
                Console.WriteLine("Expert created successfully. Redirecting to Experts...");
                TempData["SuccessMessage"] = "متخصص با موفقیت اضافه شد.";
                return RedirectToPage("Experts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in OnPostAddCategory: {Message}", ex.Message);
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"خطا در ثبت متخصص: {ex.Message} - جزئیات: {ex.InnerException?.Message}");
                await LoadFormData(cancellationToken);
                return Page();
            }
        }

        private async Task LoadFormData(CancellationToken cancellationToken)
        {
            AllServices = await _serviceAppServices.GetAllServicesAsync(cancellationToken);
            var cities = await _cityService.GetAll(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");
            var usedAppUserIds = await _expertAppServices.GetAll(cancellationToken)
                .ContinueWith(t => t.Result.Select(e => e.AppUserId).ToList(), cancellationToken);
            var availableUsers = await _userManager.Users
                .Where(u => !usedAppUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync(cancellationToken);
            AvailableUsers = new SelectList(availableUsers, "Id", "UserName");
        }
    }
}
