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

        [HttpPost("create")]
        public async Task<IActionResult> CreateExpert(
            [FromForm] ExpertCreateDto expertCreate,
            [FromForm] IFormFile? image,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _expertAppServices.CreateAsync(expertCreate, image, cancellationToken);

            if (!result)
                return BadRequest("خطا در ثبت متخصص: مشکل در آپلود عکس یا ذخیره اطلاعات");

            return Ok(new { message = "متخصص با موفقیت اضافه شد" });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Expert>>> GetAllExperts(CancellationToken cancellationToken)
        {
            var experts = await _expertAppServices.GetAllAsync(cancellationToken);
            return Ok(experts);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpert(int id, CancellationToken cancellationToken)
        {
            var result = await _expertAppServices.DeleteAsync(id, cancellationToken);

            if (!result)
                return BadRequest("حذف متخصص با خطا مواجه شد.");

            return Ok(new { message = "متخصص با موفقیت حذف شد" });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpertUpdateDto>> GetExpertById(int id, CancellationToken cancellationToken)
        {
            var expert = await _expertAppServices.ExpertUpdateInfoAsync(id, cancellationToken);
            if (expert == null)
                return NotFound("متخصص پیدا نشد.");

            return Ok(expert);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExpert(
            [FromForm] ExpertUpdateDto expertUpdate,
            [FromForm] IFormFile? image,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _expertAppServices.UpdateAsync(expertUpdate, image, cancellationToken);

            if (!result)
                return BadRequest("خطایی در آپدیت اطلاعات رخ داد.");

            return Ok(new { message = "اطلاعات متخصص با موفقیت آپدیت شد" });
        }

        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData(CancellationToken cancellationToken)
        {
            var cities = await _cityService.GetAllAsync(cancellationToken);
            var services = await _serviceAppServices.GetAllServicesAsync(cancellationToken);

            return Ok(new { cities, services });
        }

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
