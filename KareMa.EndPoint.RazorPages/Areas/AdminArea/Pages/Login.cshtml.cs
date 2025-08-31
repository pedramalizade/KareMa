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
            _logger.LogInformation("صفحه ورود بارگذاری شد.");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Page("/AdminArea/AddCustomer") ?? "/AdminArea/AddCustomer";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                Input.Password,
                isPersistent: true,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "/AdminArea/AddCustomer";
                }
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
            return Page();
        }
    }
}
