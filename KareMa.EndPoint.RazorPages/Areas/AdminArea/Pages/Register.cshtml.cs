namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{

    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly AppDbContext _dbContext;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            ILogger<RegisterModel> logger,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "نام اجباری است")]
            [MaxLength(20, ErrorMessage = "نام نمی‌تواند بیشتر از 20 کاراکتر باشد")]
            [Display(Name = "نام")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "نام خانوادگی اجباری است")]
            [MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد")]
            [Display(Name = "نام خانوادگی")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "جنسیت اجباری است")]
            [Display(Name = "جنسیت")]
            public GenderEnum Gender { get; set; }

            [Display(Name = "موجودی")]
            public decimal Balance { get; set; } = 0; 

            [Required(ErrorMessage = "ایمیل اجباری است")]
            [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "رمز عبور اجباری است")]
            [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تأیید رمز عبور")]
            [Compare("Password", ErrorMessage = "رمز عبور و تأیید آن مطابقت ندارند.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet()
        {
            _logger.LogInformation("صفحه ثبت‌نام بارگذاری شد.");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("فرآیند ثبت‌نام شروع شد.");

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return Page();
            }

            var user = new AppUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("کاربر با ایمیل {Email} با موفقیت ایجاد شد.", Input.Email);

                await EnsureAdminRoleExistsAsync();
                await _userManager.AddToRoleAsync(user, "Admin");

                var admin = new Admin
                {
                    AppUserId = user.Id,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Gender = Input.Gender,
                    Balance = Input.Balance
                };
                _dbContext.Admins.Add(admin);
                await _dbContext.SaveChangesAsync();

                await _signInManager.SignInAsync(user, isPersistent: true);

                TempData["Success"] = "ادمین جدید با موفقیت ثبت شد!";
                return LocalRedirect("/AdminArea/AddCustomer");
            }

            AddIdentityErrors(result);
            return Page();
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

        private async Task EnsureAdminRoleExistsAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                _logger.LogInformation("نقش 'Admin' یافت نشد، در حال ایجاد...");
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
                _logger.LogInformation("نقش 'Admin' با موفقیت ایجاد شد.");
            }
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogWarning("خطا در ثبت‌نام: {Error}", error.Description);
                ModelState.AddModelError("", $"خطا در ثبت‌نام: {error.Description}");
            }
        }
    }
}

