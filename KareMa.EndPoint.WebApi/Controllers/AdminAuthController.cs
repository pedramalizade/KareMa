using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AdminAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _dbContext;

        public AdminAuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public class LoginRequest
        {
            [Required] public string UserName { get; set; }
            [Required] public string Password { get; set; }
        }

        public class RegisterRequest
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required][EmailAddress] public string Email { get; set; }
            [Required] public string Password { get; set; }
            [Required] public string ConfirmPassword { get; set; }
        }

        public class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string? RedirectUrl { get; set; }
        }

        /// <summary>
        /// عملیات لاگین کاربر با استفاده از نام کاربری و رمز عبور.
        /// </summary>
        /// <param name="request">اطلاعات نام کاربری و رمز عبور.</param>
        /// <param name="returnUrl">آدرس اختیاری برای ریدایرکت پس از ورود.</param>
        /// <returns>نتیجه ورود شامل وضعیت موفقیت، پیام و آدرس ریدایرکت.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new LoginResponse { Success = false, Message = errors });
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "نام کاربری یا کلمه عبور اشتباه است" });

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new LoginResponse { Success = false, Message = "نام کاربری یا کلمه عبور اشتباه است" });

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "ورود موفقیت‌آمیز بود",
                RedirectUrl = returnUrl ?? "/AdminArea/Index"
            });
        }

        /// <summary>
        /// ثبت‌نام و ایجاد کاربر جدید از نوع ادمین.
        /// </summary>
        /// <param name="request">اطلاعات موردنیاز برای ثبت‌نام کاربر.</param>
        /// <returns>وضعیت عملیات ثبت‌نام به‌همراه پیام خطا یا موفقیت.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Password != request.ConfirmPassword)
                return BadRequest(new { Success = false, Message = "رمز عبور و تأیید آن مطابقت ندارند" });

            var user = new AppUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(new { Success = false, Errors = result.Errors.Select(e => e.Description) });

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });

            await _userManager.AddToRoleAsync(user, "Admin");

            var admin = new Admin
            {
                AppUserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Balance = 0
            };

            _dbContext.Admins.Add(admin);
            await _dbContext.SaveChangesAsync();
            await _signInManager.SignInAsync(user, isPersistent: true);

            return Ok(new { Success = true, Message = "ادمین با موفقیت ثبت شد" });
        }

        /// <summary>
        /// خروج کاربر از سیستم.
        /// </summary>
        /// <returns>نتیجه عملیات خروج.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "خروج موفقیت‌آمیز بود" });
        }
    }
}
