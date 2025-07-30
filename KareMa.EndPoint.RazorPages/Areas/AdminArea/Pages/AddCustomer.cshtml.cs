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
            Console.WriteLine("OnGet called.");
            CustomerCreate.Addresses = new Address();
            var cities = await _cityService.GetAll(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
        {
            Console.WriteLine("OnPostAddCategory started.");

            try
            {
                Console.WriteLine("Checking ModelState...");

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState is invalid.");
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
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    Console.WriteLine("Returning page with validation errors.");
                    return Page();
                }

                Console.WriteLine("ModelState is valid. Proceeding to create customer...");

                // چک کردن وضعیت لاگین
                Console.WriteLine($"IsAuthenticated: {User.Identity.IsAuthenticated}");
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
                    Console.WriteLine("No logged-in user found.");
                    ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً اول وارد شوید.");
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                CustomerCreate.AppUserId = currentUser.Id;
                Console.WriteLine($"AppUserId set to {currentUser.Id}");

                var result = await _customerAppServices.Create(CustomerCreate, Image, cancellationToken);
                if (!result)
                {
                    Console.WriteLine("Customer creation failed.");
                    ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود عکس یا ذخیره اطلاعات");
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                Console.WriteLine("Customer created successfully. Redirecting to Customers...");
                return RedirectToPage("Customers");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                ModelState.AddModelError("", $"خطا در ثبت مشتری: {ex.Message}");
                var cities = await _cityService.GetAll(cancellationToken);
                Cities = new SelectList(cities, "Id", "Name");
                return Page();
            }
        }
    }
}