using KareMa.Domain.Core.DTOs.SuggestionDTO;
using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.WebApi.Controllers
{

    [Authorize(Roles = "Expert")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionAppServices _suggestionAppServices;

        public SuggestionController(ISuggestionAppServices suggestionAppServices)
        {
            _suggestionAppServices = suggestionAppServices;
        }

        public class CreateSuggestionRequest
        {
            [Required]
            public SuggestionCreateDto SuggestionCreate { get; set; }

            [Required(ErrorMessage = "تاریخ نمی‌تواند بدون مقدار باشد")]
            [RegularExpression(@"^((1[34]\d{2}|140[0-3])/(0[1-9]|1[0-2])/(0[1-9]|[12]\d|3[01]) (2[0-3]|[01]\d):([0-5]\d):([0-5]\d))$",
                ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd hh:mm:ss باشد.")]
            [StringLength(19, MinimumLength = 19, ErrorMessage = "تاریخ باید دقیقا 19 کاراکتر باشد")]
            public string SuggestionDate { get; set; }

            [Required]
            public int OrderId { get; set; }
        }

        /// <summary>
        /// گرفتن OrderId
        /// </summary>
        [HttpGet("GetOrderId/{id:int}")]
        public IActionResult GetOrderId(int id)
        {
            return Ok(new { OrderId = id });
        }

        /// <summary>
        /// ایجاد پیشنهاد جدید
        /// </summary>
        [HttpPost("CreateSuggestion")]
        public async Task<IActionResult> CreateSuggestionAsync([FromBody] CreateSuggestionRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
            if (!int.TryParse(expertIdClaim, out var expertId))
                return Unauthorized("شناسه کارشناس یافت نشد.");

            model.SuggestionCreate.ExpertId = expertId;
            model.SuggestionCreate.OrderId = model.OrderId;

            await _suggestionAppServices.CreateAsync(model.SuggestionCreate, model.SuggestionDate, cancellationToken);

            return Ok(new { Message = "پیشنهاد با موفقیت ثبت شد." });
        }
    }
}
