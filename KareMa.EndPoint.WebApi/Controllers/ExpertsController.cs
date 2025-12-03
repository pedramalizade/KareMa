namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpertsController : ControllerBase
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ICityAppServices _cityService;
        private readonly UserManager<AppUser> _userManager;

        public ExpertsController(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices,
            ICityAppServices cityService,
            UserManager<AppUser> userManager)
        {
            _expertAppServices = expertAppServices;
            _serviceAppServices = serviceAppServices;
            _cityService = cityService;
            _userManager = userManager;
        }
        /// <summary>
        /// ایجاد متخصص جدید همراه با امکان آپلود تصویر.
        /// </summary>
        /// <param name="expertCreate">اطلاعات ثبت متخصص.</param>
        /// <param name="image">فایل تصویر متخصص (اختیاری).</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات ایجاد متخصص.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateExpert([FromForm] ExpertCreateDto expertCreate,[FromForm] IFormFile? image,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _expertAppServices.CreateAsync(expertCreate, image, cancellationToken);

            if (!result)
                return BadRequest("خطا در ثبت متخصص: مشکل در آپلود عکس یا ذخیره اطلاعات");

            return Ok(new { message = "متخصص با موفقیت اضافه شد" });
        }

        /// <summary>
        /// دریافت لیست تمام متخصص‌ها.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست متخصص‌ها.</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<Expert>>> GetAllExperts(CancellationToken cancellationToken)
        {
            var experts = await _expertAppServices.GetAllAsync(cancellationToken);
            return Ok(experts);
        }

        /// <summary>
        /// حذف متخصص بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه متخصص.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات حذف.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpert(int id, CancellationToken cancellationToken)
        {
            var result = await _expertAppServices.DeleteAsync(id, cancellationToken);

            if (!result)
                return BadRequest("حذف متخصص با خطا مواجه شد.");

            return Ok(new { message = "متخصص با موفقیت حذف شد" });
        }
        /// <summary>
        /// دریافت اطلاعات متخصص برای ویرایش.
        /// </summary>
        /// <param name="id">شناسه متخصص.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>اطلاعات قابل ویرایش متخصص.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpertUpdateDto>> GetExpertById(int id, CancellationToken cancellationToken)
        {
            var expert = await _expertAppServices.ExpertUpdateInfoAsync(id, cancellationToken);
            if (expert == null)
                return NotFound("متخصص پیدا نشد.");

            return Ok(expert);
        }


        /// <summary>
        /// ویرایش اطلاعات متخصص همراه با امکان تغییر تصویر.
        /// </summary>
        /// <param name="expertUpdate">اطلاعات ویرایش متخصص.</param>
        /// <param name="image">تصویر جدید متخصص (اختیاری).</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات آپدیت.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateExpert([FromForm] ExpertUpdateDto expertUpdate,[FromForm] IFormFile? image,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _expertAppServices.UpdateAsync(expertUpdate, image, cancellationToken);

            if (!result)
                return BadRequest("خطایی در آپدیت اطلاعات رخ داد.");

            return Ok(new { message = "اطلاعات متخصص با موفقیت آپدیت شد" });
        }

        /// <summary>
        /// دریافت داده‌های موردنیاز فرم ثبت یا ویرایش متخصص.
        /// شامل شهرها و سرویس‌ها.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست شهرها و سرویس‌ها.</returns>
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData(CancellationToken cancellationToken)
        {
            var cities = await _cityService.GetAllAsync(cancellationToken);
            var services = await _serviceAppServices.GetAllServicesAsync(cancellationToken);

            return Ok(new { cities, services });
        }

        /// <summary>
        /// دریافت داده‌های موردنیاز فرم ثبت یا ویرایش متخصص.
        /// شامل شهرها و سرویس‌ها.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست شهرها و سرویس‌ها.</returns>
        [HttpGet("available-users")]
        public async Task<IActionResult> GetAvailableUsers(CancellationToken cancellationToken)
        {
            var usedAppUserIds = await _expertAppServices.GetAllAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(e => e.AppUserId).ToList(), cancellationToken);

            var availableUsers = await _userManager.Users
                .Where(u => !usedAppUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync(cancellationToken);

            return Ok(availableUsers);
        }
    }
}
