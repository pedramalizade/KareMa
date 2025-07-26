using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using KareMa.Infra.SqlServer.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
            public decimal Balance { get; set; } = 0; // پیش‌فرض 0

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
            _logger.LogInformation("Register page loaded.");
            Console.WriteLine("Register page loaded.");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Register attempt started.");
            Console.WriteLine("Register attempt started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state.");
                Console.WriteLine("Invalid model state.");
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            var user = new AppUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created successfully.", Input.Email);
                Console.WriteLine($"User {Input.Email} created successfully.");

                // اضافه کردن نقش Admin
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
                }
                await _userManager.AddToRoleAsync(user, "Admin");

                // ثبت اطلاعات در جدول Admin
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

                // لاگین کردن کاربر بعد از ثبت‌نام
                await _signInManager.SignInAsync(user, isPersistent: true);

                TempData["Success"] = "ادمین جدید ثبت شد!";
                return LocalRedirect("/AdminArea/AddCustomer");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                _logger.LogWarning("Registration error: {Error}", error.Description);
                Console.WriteLine($"Registration error: {error.Description}");
            }

            return Page();
        }
    }


}

