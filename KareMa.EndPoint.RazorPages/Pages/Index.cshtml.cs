namespace KareMa.EndPoint.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public IndexModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; } = new List<CategoryNameDto>();

        [BindProperty]
        public string SearchQuery { get; set; }

        public string SearchMessage { get; set; }

        public bool IsTemporaryMessage { get; set; } 

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            CategoryNames = await _categoryAppServices.GetCategorisName(cancellationToken);
        }

        public async Task<IActionResult> OnPostSearchAsync(CancellationToken cancellationToken)
        {
            var allCategories = await _categoryAppServices.GetCategorisName(cancellationToken);
            CategoryNames = allCategories;

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchMessage = "شما هنوز چیزی برای جستجو وارد نکردید!";
                IsTemporaryMessage = true;
                return Page();
            }

            var searchResults = allCategories
                .Where(c => c.Name.Trim().ToLower().Contains(SearchQuery.Trim().ToLower()))
                .ToList();

            if (!searchResults.Any())
            {
                SearchMessage = $"متأسفیم، '{SearchQuery}' وجود ندارد!";
                IsTemporaryMessage = true;
                CategoryNames = allCategories;
                return Page();
            }

            CategoryNames = searchResults;
            SearchMessage = $"نتایج جستجو برای '{SearchQuery}'";
            IsTemporaryMessage = false;
            return Page();
        }
    }
}
