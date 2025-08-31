namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddCustomerModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityService;
        private readonly UserManager<AppUser> _userManager;

        public AddCustomerModel(ICustomerAppServices customerAppServices, ICityAppServices cityService, UserManager<AppUser> userManager)
        {
            _customerAppServices = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [BindProperty]
        public CustomerCreateDto CustomerCreate { get; set; } = new CustomerCreateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        public SelectList Cities { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            CustomerCreate.Addresses = new Address();
            var cities = await _cityService.GetAllAsync(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessages = new List<string>();
                    foreach (var key in ModelState.Keys)
                    {
                        var entry = ModelState[key];
                        foreach (var error in entry.Errors)
                        {
                            errorMessages.Add($"خطا در {key}: {error.ErrorMessage}");
                        }
                    }

                    ModelState.AddModelError("", "لطفاً خطاهای زیر را برطرف کنید: " + string.Join(" | ", errorMessages));
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                if (!User.Identity.IsAuthenticated)
                {
                    ModelState.AddModelError("", "کاربر احراز هویت نشده است.");
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً ابتدا وارد شوید.");
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                CustomerCreate.AppUserId = currentUser.Id;

                var result = await _customerAppServices.CreateAsync(CustomerCreate, Image, cancellationToken);
                if (!result)
                {
                    ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود تصویر یا ذخیره اطلاعات.");
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                return RedirectToPage("Customers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطا در ثبت مشتری: {ex.Message}");
                var cities = await _cityService.GetAllAsync(cancellationToken);
                Cities = new SelectList(cities, "Id", "Name");
                return Page();
            }
        }
    }
}