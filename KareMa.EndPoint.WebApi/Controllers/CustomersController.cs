using KareMa.Domain.Core.DTOs.CustomerDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;

        public CustomersController(ICustomerAppServices customerAppServices, ICityAppServices cityAppService)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(
            [FromForm] CustomerCreateDto customerCreate,
            [FromForm] IFormFile? image,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerAppServices.CreateAsync(customerCreate, image, cancellationToken);

            if (!result)
                return BadRequest("خطا در ثبت مشتری");

            return Ok(new { message = "مشتری با موفقیت ثبت شد" });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetCustomerDto>>> GetAllCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerAppServices.GetAllAsync(cancellationToken);
            return Ok(customers);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var result = await _customerAppServices.DeleteAsync(id, cancellationToken);

            if (!result)
                return BadRequest("حذف مشتری با خطا مواجه شد.");

            return Ok(new { message = "مشتری با موفقیت حذف شد" });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerUpdateDto>> GetCustomerById(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerAppServices.GetCustomerUpdateInfoAsync(id, cancellationToken);

            if (customer == null)
                return NotFound("مشتری پیدا نشد.");

            return Ok(customer);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer(
            [FromForm] CustomerUpdateDto customerUpdate,
            [FromForm] IFormFile? image,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerAppServices.UpdateAsync(customerUpdate, image, cancellationToken);

            if (!result)
                return BadRequest("خطایی در آپدیت اطلاعات رخ داد.");

            return Ok(new { message = "اطلاعات مشتری با موفقیت آپدیت شد" });
        }

        [HttpGet("cities")]
        public async Task<ActionResult<List<City>>> GetCities(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllAsync(cancellationToken);
            return Ok(cities);
        }
    }
}
