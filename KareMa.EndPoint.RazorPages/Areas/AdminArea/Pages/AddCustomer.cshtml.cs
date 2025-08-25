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
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var value = ModelState[modelStateKey];
                        foreach (var error in value.Errors)
                        {
                            var errorMessage = $"Key: {modelStateKey}, Error: {error.ErrorMessage}";
                            Console.WriteLine(errorMessage);
                            errorMessages.Add(errorMessage);
                        }
                    }
                    ModelState.AddModelError("", "لطفاً خطاهای زیر را بررسی کنید: " + string.Join(" | ", errorMessages));
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                if (User.Identity.IsAuthenticated)
                {
                    Console.WriteLine($"User Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
                }
                else
                {
                    Console.WriteLine("User is not authenticated.");
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً اول وارد شوید.");
                    var cities = await _cityService.GetAllAsync(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                CustomerCreate.AppUserId = currentUser.Id;

                var result = await _customerAppServices.CreateAsync(CustomerCreate, Image, cancellationToken);
                if (!result)
                {
                    ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود عکس یا ذخیره اطلاعات");
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