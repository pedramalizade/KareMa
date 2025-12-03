using KareMa.Domain.Core.DTOs.ServiceDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ISubCategoryAppServices _subCategoryAppServices;

        public ServicesController(
            IServiceAppServices serviceAppServices,
            ISubCategoryAppServices subCategoryAppServices)
        {
            _serviceAppServices = serviceAppServices;
            _subCategoryAppServices = subCategoryAppServices;
        }

        /// <summary>
        /// ایجاد یک سرویس جدید
        /// </summary>
        /// <param name="serviceCreate">مدل حاوی اطلاعات سرویس جدید</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا در ایجاد سرویس</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateService([FromBody] ServiceCreateDto serviceCreate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceAppServices.CreateAsync(serviceCreate, cancellationToken);
            return Ok(new { message = "سرویس با موفقیت ایجاد شد." });
        }

        /// <summary>
        /// دریافت همه سرویس‌ها
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست تمام سرویس‌ها</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetServiceDto>>> GetAllServices(CancellationToken cancellationToken)
        {
            var services = await _serviceAppServices.GetAllAsync(cancellationToken);
            return Ok(services);
        }

        /// <summary>
        /// حذف یک سرویس با شناسه مشخص
        /// </summary>
        /// <param name="id">شناسه سرویس</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا در حذف سرویس</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteService(int id, CancellationToken cancellationToken)
        {
            await _serviceAppServices.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "سرویس حذف شد." });
        }

        /// <summary>
        /// دریافت اطلاعات یک سرویس برای بروزرسانی
        /// </summary>
        /// <param name="id">شناسه سرویس</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>مدل اطلاعات سرویس یا پیغام خطا در صورت عدم وجود</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceUpdateDto>> GetServiceById(int id, CancellationToken cancellationToken)
        {
            var service = await _serviceAppServices.ServiceUpdateInfoAsync(id, cancellationToken);
            if (service == null)
                return NotFound("سرویس پیدا نشد.");
            return Ok(service);
        }

        /// <summary>
        /// بروزرسانی اطلاعات یک سرویس
        /// </summary>
        /// <param name="serviceUpdate">مدل حاوی اطلاعات جدید سرویس</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا بعد از بروزرسانی</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateService([FromBody] ServiceUpdateDto serviceUpdate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceAppServices.UpdateAsync(serviceUpdate, cancellationToken);
            return Ok(new { message = "سرویس با موفقیت آپدیت شد." });
        }

        /// <summary>
        /// دریافت نام زیرشاخه‌های سرویس‌ها
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست نام زیرشاخه‌ها</returns>
        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubCategories(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppServices.GetCategorisNameAsync(cancellationToken);
            return Ok(subCategories);
        }
    }
}
