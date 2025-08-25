namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class ExpertsModel : PageModel
    {
        private readonly IExpertAppServices _expertAppService;
        public ExpertsModel(IExpertAppServices expertAppService)
        {
            _expertAppService = expertAppService;
        }

        [BindProperty]
        public List<Expert> GetExpert { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            GetExpert = await _expertAppService.GetAllAsync(cancellationToken);
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            var result = await _expertAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                TempData["ErrorMessage"] = "حذف متخصص با خطا مواجه شد.";
            }
            else
            {
                TempData["SuccessMessage"] = "متخصص با موفقیت حذف شد.";
            }
            return RedirectToPage();
        }
    }
}
