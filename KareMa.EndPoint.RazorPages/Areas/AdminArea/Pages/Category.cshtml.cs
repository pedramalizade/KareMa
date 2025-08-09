namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoryModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [BindProperty]
        public List<GetCategoryDto> GetCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("CategoryModel.OnGetAsync called");
            GetCategories = await _categoryAppServices.GetAllAsync(cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryAppServices.DeleteAsync(id, cancellationToken);
            return RedirectToPage(new { refresh = DateTime.Now.Ticks }); 
        }
    }
}
