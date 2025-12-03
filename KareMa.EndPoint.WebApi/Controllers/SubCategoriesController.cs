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

        /// <summary>
        /// ایجاد یک زیرشاخه جدید
        /// </summary>
        /// <param name="subCategoryCreate">مدل حاوی اطلاعات زیرشاخه جدید</param>
        /// <param name="image">تصویر مربوط به زیرشاخه</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا در ایجاد زیرشاخه</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateSubCategory([FromForm] SubCategoryCreateDto subCategoryCreate, IFormFile image, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _subCategoryAppServices.CreateAsync(subCategoryCreate, cancellationToken, image);
            return Ok(new { message = "SubCategory با موفقیت ایجاد شد." });
        }

        /// <summary>
        /// دریافت همه زیرشاخه‌ها
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست تمام زیرشاخه‌ها</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetSubCategoryDto>>> GetAllSubCategories(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppServices.GetSubCategoriesAsync(cancellationToken);
            return Ok(subCategories);
        }

        /// <summary>
        /// حذف یک زیرشاخه با شناسه مشخص
        /// </summary>
        /// <param name="id">شناسه زیرشاخه</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا در حذف زیرشاخه</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            await _subCategoryAppServices.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "SubCategory حذف شد." });
        }

        /// <summary>
        /// دریافت اطلاعات یک زیرشاخه برای بروزرسانی
        /// </summary>
        /// <param name="id">شناسه زیرشاخه</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>مدل اطلاعات زیرشاخه یا پیغام خطا در صورت عدم وجود</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategoryUpdateDto>> GetSubCategoryById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) return BadRequest("Invalid SubCategory Id");

            var subCategory = await _subCategoryAppServices.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);
            if (subCategory == null)
                return NotFound("SubCategory پیدا نشد.");

            return Ok(subCategory);
        }

        /// <summary>
        /// بروزرسانی اطلاعات یک زیرشاخه
        /// </summary>
        /// <param name="subCategoryUpdate">مدل حاوی اطلاعات جدید زیرشاخه</param>
        /// <param name="image">تصویر جدید (اختیاری)</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا بعد از بروزرسانی</returns>
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

        /// <summary>
        /// دریافت لیست دسته‌بندی‌ها برای انتخاب در فرم زیرشاخه
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست نام دسته‌بندی‌ها</returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppServices.GetCategorisNameAsync(cancellationToken);
            return Ok(categories);
        }
    }
}
