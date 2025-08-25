namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "ایمیل اجباری است")]
            [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
            public string Email { get; set; }

            [Required(ErrorMessage = "رمز عبور اجباری است")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
            _logger.LogInformation("Login page loaded.");
            Console.WriteLine("Login page loaded.");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Page("/AdminArea/AddCustomer") ?? "/AdminArea/AddCustomer"; 

            _logger.LogInformation("Login attempt started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state.");
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", Input.Email);
                ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully.", Input.Email);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    _logger.LogWarning("returnUrl is null or empty, using default.");
                    returnUrl = "/AdminArea/AddCustomer";
                }

                return LocalRedirect(returnUrl);
            }

            _logger.LogWarning("Login failed for {Email}.", Input.Email);
            ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
            return Page();
        }
    }
}
