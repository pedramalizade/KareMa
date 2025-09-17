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

        [HttpPost("create")]
        public async Task<IActionResult> CreateService([FromBody] ServiceCreateDto serviceCreate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceAppServices.CreateAsync(serviceCreate, cancellationToken);
            return Ok(new { message = "سرویس با موفقیت ایجاد شد." });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetServiceDto>>> GetAllServices(CancellationToken cancellationToken)
        {
            var services = await _serviceAppServices.GetAllAsync(cancellationToken);
            return Ok(services);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteService(int id, CancellationToken cancellationToken)
        {
            await _serviceAppServices.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "سرویس حذف شد." });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceUpdateDto>> GetServiceById(int id, CancellationToken cancellationToken)
        {
            var service = await _serviceAppServices.ServiceUpdateInfoAsync(id, cancellationToken);
            if (service == null)
                return NotFound("سرویس پیدا نشد.");
            return Ok(service);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateService([FromBody] ServiceUpdateDto serviceUpdate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceAppServices.UpdateAsync(serviceUpdate, cancellationToken);
            return Ok(new { message = "سرویس با موفقیت آپدیت شد." });
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubCategories(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppServices.GetCategorisNameAsync(cancellationToken);
            return Ok(subCategories);
        }
    }
}
