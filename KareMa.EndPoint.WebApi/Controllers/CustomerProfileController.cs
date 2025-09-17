using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;

        public CustomerProfileController(
            ICustomerAppServices customerAppServices,
            ICityAppServices cityAppService)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
        }

        // ************* GET معادل OnGetAsync *************

        /// <summary>
        /// دریافت اطلاعات پروفایل مشتری
        /// </summary>
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfileAsync(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
                return Unauthorized("کاربر معتبر نیست.");

            var customerUpdate = await _customerAppServices.GetCustomerUpdateInfoAsync(userCustomerId, cancellationToken);
            if (customerUpdate == null)
                return NotFound("اطلاعات مشتری یافت نشد.");

            var cities = await _cityAppService.GetAllAsync(cancellationToken);

            return Ok(new
            {
                CustomerUpdate = customerUpdate,
                Cities = cities
            });
        }

        // ************* POST معادل OnPostUpdateAsync *************

        public class UpdateCustomerProfileRequest
        {
            [Required]
            public CustomerUpdateDto CustomerUpdate { get; set; }

            public IFormFile? Image { get; set; }
        }

        /// <summary>
        /// بروزرسانی پروفایل مشتری
        /// </summary>
        [HttpPost("UpdateProfile")]
        [RequestSizeLimit(10_000_000)] 
        public async Task<IActionResult> UpdateProfileAsync([FromForm] UpdateCustomerProfileRequest model, CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
                return Unauthorized("کاربر معتبر نیست.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerAppServices.UpdateProfileAsync(userCustomerId, model.CustomerUpdate, model.Image, cancellationToken);
            if (!result.Success)
                return BadRequest(new { Error = result.ErrorMessage });

            return Ok(new { Message = "تغییرات با موفقیت ذخیره شد." });
        }

        /// <summary>
        /// دریافت لیست شهرها 
        /// </summary>
        [HttpGet("Cities")]
        public async Task<IActionResult> GetCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            return Ok(cities);
        }
    }
}
