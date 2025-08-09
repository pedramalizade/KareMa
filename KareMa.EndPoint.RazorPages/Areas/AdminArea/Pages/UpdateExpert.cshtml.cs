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
            Console.WriteLine($"OnGetAsync called with id: {id}");
            ExpertUpdate = await _expertAppServices.ExpertUpdateInfoAsync(id, cancellationToken);
            if (ExpertUpdate == null)
            {
                Console.WriteLine($"Expert with ID: {id} not found.");
                TempData["ErrorMessage"] = "متخصص پیدا نشد.";
                return RedirectToPage("Experts");
            }
            Console.WriteLine($"Loaded expert with ID: {ExpertUpdate.Id}, Name: {ExpertUpdate.FirstName} {ExpertUpdate.LastName}");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnPostUpdateAsync called with Expert ID: {ExpertUpdate.Id}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
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
                Console.WriteLine($"Error in OnPostUpdate: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                TempData["ErrorMessage"] = $"خطا در آپدیت: {ex.Message}";
                return Page();
            }
        }
    }
}