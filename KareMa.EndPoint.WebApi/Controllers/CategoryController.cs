using KareMa.Domain.Core.DTOs.CategoryDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin")] 
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoryController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        /// <summary>
        /// ایجاد یک دسته‌بندی جدید همراه با تصویر.
        /// </summary>
        /// <param name="categoryCreate">اطلاعات موردنیاز برای ایجاد دسته‌بندی.</param>
        /// <param name="image">فایل تصویر دسته‌بندی.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات ایجاد دسته‌بندی.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto categoryCreate,[FromForm] IFormFile image,CancellationToken cancellationToken)
        {
            if (image == null || image.Length == 0)
                return BadRequest("لطفاً یک تصویر انتخاب کنید.");

            await _categoryAppServices.CreateAsync(categoryCreate, image, cancellationToken);
            return Ok(new { message = "دسته‌بندی با موفقیت ایجاد شد." });
        }


        /// <summary>
        /// دریافت لیست تمام دسته‌بندی‌ها.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست دسته‌بندی‌ها.</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetCategoryDto>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppServices.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        /// <summary>
        /// حذف یک دسته‌بندی بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه دسته‌بندی.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات حذف.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            await _categoryAppServices.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "دسته‌بندی حذف شد." });
        }

        /// <summary>
        /// دریافت اطلاعات دسته‌بندی برای ویرایش.
        /// </summary>
        /// <param name="id">شناسه دسته‌بندی.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>اطلاعات قابل ویرایش دسته‌بندی.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryUpdateDto>> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var categoryUpdate = await _categoryAppServices.ServiceCategoryUpdateInfoAsync(id, cancellationToken);
            if (categoryUpdate == null)
                return NotFound("دسته‌بندی پیدا نشد.");

            return Ok(categoryUpdate);
        }

        /// <summary>
        /// ویرایش دسته‌بندی موجود همراه با امکان تغییر تصویر.
        /// </summary>
        /// <param name="categoryUpdate">اطلاعات ویرایشی دسته‌بندی.</param>
        /// <param name="image">تصویر جدید (اختیاری).</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>نتیجه عملیات آپدیت.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(
            [FromForm] CategoryUpdateDto categoryUpdate,
            [FromForm] IFormFile? image,
            CancellationToken cancellationToken)
        {
            var result = await _categoryAppServices.UpdateAsync(categoryUpdate, image, cancellationToken);

            if (!result)
                return BadRequest("خطایی در آپدیت دسته‌بندی رخ داد.");

            return Ok(new { message = "دسته‌بندی با موفقیت آپدیت شد." });
        }
    }
}
