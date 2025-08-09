namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class SubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        public SubCategoryModel(ISubCategoryAppServices seubCategoryAppServices)
        {
            _subCategoryAppServices = seubCategoryAppServices;
        }
        [BindProperty]
        public List<GetSubCategoryDto> SubCategories { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            SubCategories = await _subCategoryAppServices.GetSubCategoriesAsync(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _subCategoryAppServices.DeleteAsync(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
