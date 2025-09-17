using KareMa.Domain.Core.DTOs.CustomerDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppServices _customerAppServices;

        public CustomerController(ICustomerAppServices customerAppServices)
        {
            _customerAppServices = customerAppServices;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<CustomerSummaryDto>> GetCustomerSummary(CancellationToken cancellationToken)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userCustomerId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out var userId))
                return BadRequest("شناسه کاربر معتبر نیست.");

            var summary = await _customerAppServices.GetCustomerSummaryAsync(userId, cancellationToken);

            if (summary == null)
                return NotFound();

            return Ok(summary);
        }
    }
}
