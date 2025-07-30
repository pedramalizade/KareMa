namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class AddSubCategoryModel : PageModel
    {

        private readonly ISubCategoryAppServices _subCategoryAppServices;
        private readonly ICategoryAppServices _categoryAppServices;

        public AddSubCategoryModel(ISubCategoryAppServices subCategoryAppServices, ICategoryAppServices categoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
            _categoryAppServices = categoryAppServices;
        }

        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; }

        [BindProperty]
        public SubCategoryCreateDto ServiceSubCategoryCreate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = " عکس نمی‌تواند بدون مقدار باشد")]
        public IFormFile Image { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            CategoryNames = await _categoryAppServices.GetCategorisName(cancellationToken);
        }

        public async Task<IActionResult> OnPostAdd(SubCategoryCreateDto serviceSubCategoryCreate, CancellationToken cancellationToken, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryAppServices.Create(serviceSubCategoryCreate, cancellationToken, image);
                return RedirectToPage("SubCategory");
            }
            return Page();
        }
    }
}
