namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class UpdateExpertModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;

        public UpdateExpertModel(IExpertAppServices expertAppServices)
        {
            _expertAppServices = expertAppServices;
        }

        [BindProperty]
        public ExpertUpdateDto ExpertUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            ExpertUpdate = await _expertAppServices.ExpertUpdateInfoAsync(id, cancellationToken);
            if (ExpertUpdate == null)
            {
                TempData["ErrorMessage"] = "متخصص پیدا نشد.";
                return RedirectToPage("Experts");
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
                var result = await _expertAppServices.UpdateAsync(ExpertUpdate, Image, cancellationToken);
                if (result)
                {
                    TempData["SuccessMessage"] = "اطلاعات متخصص با موفقیت آپدیت شد.";
                    return RedirectToPage("Experts");
                }
                else
                {
                    TempData["ErrorMessage"] = "خطایی در آپدیت اطلاعات رخ داد.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در آپدیت: {ex.Message}";
                return Page();
            }
        }
    }
}