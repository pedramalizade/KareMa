namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddExpertModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ICityAppServices _cityService;
        private readonly UserManager<AppUser> _userManager;

        public AddExpertModel(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices,
            ICityAppServices cityService,
            UserManager<AppUser> userManager)
        {
            _expertAppServices = expertAppServices ?? throw new ArgumentNullException(nameof(expertAppServices));
            _serviceAppServices = serviceAppServices ?? throw new ArgumentNullException(nameof(serviceAppServices));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [BindProperty]
        public ExpertCreateDto ExpertCreate { get; set; } = new ExpertCreateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<Service> AllServices { get; set; }

        public SelectList Cities { get; set; }
        public SelectList AvailableUsers { get; set; } 

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            ExpertCreate.Address = new Address();
            AllServices = await _serviceAppServices.GetAllServicesAsync(cancellationToken);
            var cities = await _cityService.GetAllAsync(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");

            var usedAppUserIds = await _expertAppServices.GetAllAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(e => e.AppUserId).ToList(), cancellationToken);
            var availableUsers = await _userManager.Users
                .Where(u => !usedAppUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync(cancellationToken);
            AvailableUsers = new SelectList(availableUsers, "Id", "UserName");
        }

        public async Task<IActionResult> OnPostAddCategoryAsync(CancellationToken cancellationToken)
        {
            try
            {
                ModelState.Remove("ExpertCreate.Image");
                if (!ModelState.IsValid)
                {
                    var errorMessages = ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .SelectMany(ms => ms.Value.Errors.Select(e => $"Key: {ms.Key}, Error: {e.ErrorMessage}"))
                        .ToList();
                    foreach (var error in errorMessages)
                    {
                        Console.WriteLine(error);
                    }
                    ModelState.AddModelError("", "لطفاً خطاهای زیر را بررسی کنید: " + string.Join(" | ", errorMessages));
                    await LoadFormData(cancellationToken);
                    return Page();
                }

                var result = await _expertAppServices.CreateAsync(ExpertCreate, Image, cancellationToken);
                if (!result)
                {
                    ModelState.AddModelError("", "خطا در ثبت متخصص: مشکل در آپلود عکس یا ذخیره اطلاعات");
                    await LoadFormData(cancellationToken);
                    return Page();
                }

                TempData["SuccessMessage"] = "متخصص با موفقیت اضافه شد.";
                return RedirectToPage("Experts");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطا در ثبت متخصص: {ex.Message} - جزئیات: {ex.InnerException?.Message}");
                await LoadFormData(cancellationToken);
                return Page();
            }
        }

        private async Task LoadFormData(CancellationToken cancellationToken)
        {
            AllServices = await _serviceAppServices.GetAllServicesAsync(cancellationToken);
            var cities = await _cityService.GetAllAsync(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");
            var usedAppUserIds = await _expertAppServices.GetAllAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(e => e.AppUserId).ToList(), cancellationToken);
            var availableUsers = await _userManager.Users
                .Where(u => !usedAppUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync(cancellationToken);
            AvailableUsers = new SelectList(availableUsers, "Id", "UserName");
        }
    }
}