using KareMa.Domain.Core.DTOs.Expert;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Expert")]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpertController : ControllerBase
    {
        private readonly IExpertAppServices _expertAppServices;

        public ExpertController(IExpertAppServices expertAppServices)
        {
            _expertAppServices = expertAppServices;
        }

        [HttpGet("name")]
        public async Task<ActionResult<ExpertNameDto>> GetExpertName(CancellationToken cancellationToken)
        {
            var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
            if (string.IsNullOrEmpty(expertIdClaim))
                return Unauthorized();

            if (!int.TryParse(expertIdClaim, out var expertId))
                return BadRequest("شناسه کاربر معتبر نیست.");

            var expertName = await _expertAppServices.GetExpertNameAsync(expertId, cancellationToken);

            if (expertName == null)
                return NotFound();

            return Ok(expertName);
        }
    }
}
