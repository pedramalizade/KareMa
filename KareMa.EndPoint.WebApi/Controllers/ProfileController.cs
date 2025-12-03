namespace KareMa.EndPoint.WebApi.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProfileController : ControllerBase
    {
        private readonly IAdminAppServices _adminAppServices;

        public ProfileController(IAdminAppServices adminAppServices)
        {
            _adminAppServices = adminAppServices;
        }

        /// <summary>
        /// دریافت اطلاعات پروفایل ادمین وارد شده
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>اطلاعات پروفایل ادمین در صورت موفقیت، یا پیام خطا در صورت عدم موفقیت</returns>
        [HttpGet]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            try
            {
                var adminUserIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userAdminId");
                if (adminUserIdClaim == null)
                    return Unauthorized(new { Success = false, Message = "شناسه ادمین پیدا نشد" });

                int adminUserId = int.Parse(adminUserIdClaim.Value);
                var profile = await _adminAppServices.AdminUpdateInfoAsync(adminUserId, cancellationToken);
                return Ok(new { Success = true, Data = profile });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// بروزرسانی اطلاعات پروفایل ادمین
        /// </summary>
        /// <param name="adminUpdate">مدل حاوی اطلاعات جدید ادمین</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا بعد از تلاش بروزرسانی</returns>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] AdminUpdateDto adminUpdate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { Success = false, Message = errors });
            }

            try
            {
                var adminUserIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userAdminId");
                if (adminUserIdClaim == null)
                    return Unauthorized(new { Success = false, Message = "شناسه ادمین پیدا نشد" });

                int adminUserId = int.Parse(adminUserIdClaim.Value);
                adminUpdate.Id = adminUserId;

                await _adminAppServices.UpdateAsync(adminUpdate, cancellationToken);
                return Ok(new { Success = true, Message = "اطلاعات ادمین با موفقیت بروزرسانی شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
