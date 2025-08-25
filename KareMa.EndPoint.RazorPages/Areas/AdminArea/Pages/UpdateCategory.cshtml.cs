namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class UpdateCategoryModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public UpdateCategoryModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [BindProperty]
        public CategoryUpdateDto CategoryUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            CategoryUpdate = await _categoryAppServices.ServiceCategoryUpdateInfoAsync(id, cancellationToken);
            if (CategoryUpdate == null)
            {
                TempData["ErrorMessage"] = "دسته‌بندی پیدا نشد.";
                return RedirectToPage("Category");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }

            try
            {
                var result = await _categoryAppServices.UpdateAsync(CategoryUpdate, Image, cancellationToken);
                if (result)
                {
                    TempData["SuccessMessage"] = "دسته‌بندی با موفقیت آپدیت شد.";
                   
                    Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                    Response.Headers["Pragma"] = "no-cache";
                    Response.Headers["Expires"] = "0";
                    return RedirectToPage("Category", new { refresh = DateTime.Now.Ticks });
                }
                else
                {
                    TempData["ErrorMessage"] = "خطایی در آپدیت دسته‌بندی رخ داد.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا: {ex.Message}";
                return Page();
            }
        }
    }
}
