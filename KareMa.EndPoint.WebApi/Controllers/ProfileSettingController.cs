using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Expert")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSettingController : ControllerBase
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;

        public ProfileSettingController(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices)
        {
            _expertAppServices = expertAppServices;
            _serviceAppServices = serviceAppServices;
        }

        public class UpdateProfileRequest
        {
            public ExpertUpdateDto ExpertUpdate { get; set; }
            public IFormFile? Image { get; set; }

            [Required(ErrorMessage = "تاریخ تولد نمی‌تواند بدون مقدار باشد")]
            [RegularExpression(@"^(\d{4})/(\\d{2})/(\\d{2})$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd باشد.")]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "تاریخ باید دقیقا 10 کاراکتر باشد")]
            public string BirthDate { get; set; }
        }

        /// <summary>
        /// گرفتن اطلاعات پروفایل کارشناس
        /// </summary>
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfileAsync(CancellationToken cancellationToken)
        {
            var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
            if (!int.TryParse(expertIdClaim, out var expertId))
                return Unauthorized("شناسه کارشناس یافت نشد.");

            var expertUpdate = await _expertAppServices.GetExpertUpdateAsync(expertId, cancellationToken);
            var servicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);

            var result = new
            {
                ExpertUpdate = expertUpdate,
                ServicesNames = servicesNames,
                BirthDate = expertUpdate.BirthDate.ToPersianString("yyyy/MM/dd")
            };

            return Ok(result);
        }

        /// <summary>
        /// آپدیت پروفایل کارشناس
        /// </summary>
        [HttpPost("UpdateProfile")]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> UpdateProfileAsync([FromForm] UpdateProfileRequest model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
                if (!int.TryParse(expertIdClaim, out var expertId))
                    return Unauthorized("شناسه کارشناس یافت نشد.");

                model.ExpertUpdate.Id = expertId;

                await _expertAppServices.UpdateProfileAsync(model.ExpertUpdate, model.Image, model.BirthDate, cancellationToken);

                return Ok(new { Message = "تغییرات با موفقیت ذخیره شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}