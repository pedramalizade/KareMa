namespace KareMa.EndPoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppServices _accountAppServices;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IAccountAppServices accountAppServices, SignInManager<AppUser> signInManager)
        {
            _accountAppServices = accountAppServices;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login API 
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountLoginDto accountLogin, [FromQuery] string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var succeededLogin = await _accountAppServices.Login(accountLogin);

            if (!succeededLogin)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "ایمیل یا کلمه عبور اشتباه است"
                });
            }

            var roles = await _accountAppServices.GetUserRolesByEmail(accountLogin.Email);
            return Ok(new
            {
                success = true,
                message = "ورود با موفقیت انجام شد",
                roles = roles, 
                returnUrl = returnUrl ?? "/"
            });
        }

        /// <summary>
        /// Register a new user (Customer or Expert)
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDto accountRegister, [FromQuery] string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountAppServices.Register(accountRegister);

            if (result.Count == 0)
            {
                return Ok(new
                {
                    success = true,
                    message = "ثبت نام با موفقیت انجام شد",
                    returnUrl = returnUrl ?? "/Account/Login"
                });
            }

            var errors = result.Select(e => e.Description).ToList();

            return BadRequest(new
            {
                success = false,
                message = "ثبت نام با خطا مواجه شد",
                errors = errors
            });
        }

        /// <summary>
        /// Logout API
        /// </summary>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            return Ok(new
            {
                success = true,
                message = "خروج با موفقیت انجام شد",
                returnUrl = returnUrl ?? "/"
            });
        }
    }
}
