using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.WebApi.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _dbContext;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<int>> roleManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public class LoginRequest
        {
            [Required][EmailAddress] public string Email { get; set; }
            [Required] public string Password { get; set; }
        }

        public class RegisterRequest
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required][EmailAddress] public string Email { get; set; }
            [Required] public string Password { get; set; }
            [Required] public string ConfirmPassword { get; set; }
            [Required] public string Role { get; set; }
        }

        public class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string? RedirectUrl { get; set; }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new LoginResponse { Success = false, Message = errors });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "ایمیل یا رمز عبور اشتباه است" });

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Unauthorized(new LoginResponse { Success = false, Message = "ایمیل یا رمز عبور اشتباه است" });

            string redirectUrl = returnUrl;
            var roles = await _userManager.GetRolesAsync(user);
            if (string.IsNullOrEmpty(redirectUrl))
            {
                if (roles.Contains("Customer")) redirectUrl = "/CustomerArea/Index";
                else if (roles.Contains("Expert")) redirectUrl = "/ExpertArea/Index";
                else redirectUrl = "/";
            }

            return Ok(new LoginResponse { Success = true, Message = "ورود موفقیت‌آمیز بود", RedirectUrl = redirectUrl });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "خروج موفقیت‌آمیز بود" });
        }

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

            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = request.Role });

            await _userManager.AddToRoleAsync(user, request.Role);

            if (request.Role == "Customer")
            {
                var customer = new Customer { AppUserId = user.Id, FirstName = request.FirstName, LastName = request.LastName, Balance = 0 };
                _dbContext.Customers.Add(customer);
            }
            else if (request.Role == "Expert")
            {
                var expert = new Expert { AppUserId = user.Id, FirstName = request.FirstName, LastName = request.LastName, Balance = 0 };
                _dbContext.Experts.Add(expert);
            }

            await _dbContext.SaveChangesAsync();
            await _signInManager.SignInAsync(user, isPersistent: true);

            return Ok(new { Success = true, Message = $"{request.Role} با موفقیت ثبت شد" });
        }
    }
}
