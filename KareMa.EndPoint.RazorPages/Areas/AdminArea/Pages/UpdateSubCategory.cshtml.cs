namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class UpdateSubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        private readonly ICategoryAppServices _categoryAppService;

        public UpdateSubCategoryModel(ISubCategoryAppServices subCategoryAppServices, ICategoryAppServices categoryAppService)
        {
            _subCategoryAppServices = subCategoryAppServices;
            _categoryAppService = categoryAppService;
        }

        [BindProperty]
        public SubCategoryUpdateDto SubCategoryUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; } = new List<CategoryNameDto>();

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) 
            {
                return BadRequest("Invalid Category Id");
            }

            SubCategoryUpdate = await _subCategoryAppServices.ServiceSubCategoryUpdateInfoAsync(id, cancellationToken);

            if (SubCategoryUpdate == null)
            {
                return NotFound("SubCategory not found");
            }

            CategoryNames = await _categoryAppService.GetCategorisNameAsync(cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdate(CancellationToken cancellationToken) 
        {
            if (!ModelState.IsValid)
            {
                CategoryNames = await _categoryAppService.GetCategorisNameAsync(cancellationToken); 
                return Page();
            }

            try
            {
                await _subCategoryAppServices.UpdateAsync(SubCategoryUpdate, Image, cancellationToken);
                return RedirectToPage("SubCategory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to update subcategory: {ex.Message}");
                CategoryNames = await _categoryAppService.GetCategorisNameAsync(cancellationToken);
                return Page();
            }
        }
    }
}
