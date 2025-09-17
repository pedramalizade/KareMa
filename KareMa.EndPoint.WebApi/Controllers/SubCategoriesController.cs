using KareMa.Domain.Core.DTOs.SubCategoryDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        private readonly ICategoryAppServices _categoryAppServices;

        public SubCategoriesController(
            ISubCategoryAppServices subCategoryAppServices,
            ICategoryAppServices categoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
            _categoryAppServices = categoryAppServices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSubCategory([FromForm] SubCategoryCreateDto subCategoryCreate, IFormFile image, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _subCategoryAppServices.CreateAsync(subCategoryCreate, cancellationToken, image);
            return Ok(new { message = "SubCategory با موفقیت ایجاد شد." });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetSubCategoryDto>>> GetAllSubCategories(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppServices.GetSubCategoriesAsync(cancellationToken);
            return Ok(subCategories);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            await _subCategoryAppServices.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "SubCategory حذف شد." });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategoryUpdateDto>> GetSubCategoryById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) return BadRequest("Invalid SubCategory Id");

            var subCategory = await _subCategoryAppServices.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound("SubCategory پیدا نشد.");

            return Ok(subCategory);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubCategory([FromForm] SubCategoryUpdateDto subCategoryUpdate, IFormFile? image, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _subCategoryAppServices.UpdateAsync(subCategoryUpdate, image, cancellationToken);
                return Ok(new { message = "SubCategory با موفقیت آپدیت شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در آپدیت SubCategory: {ex.Message}" });
            }
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppServices.GetCategorisNameAsync(cancellationToken);
            return Ok(categories);
        }
    }
}
