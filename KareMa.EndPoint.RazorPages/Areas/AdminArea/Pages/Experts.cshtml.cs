using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            GetExpert = await _expertAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnPostDelete called for expert ID: {id}");
            var result = await _expertAppService.Delete(id, cancellationToken);
            if (!result)
            {
                TempData["ErrorMessage"] = "حذف متخصص با خطا مواجه شد.";
                Console.WriteLine($"Delete failed for expert ID: {id}");
            }
            else
            {
                TempData["SuccessMessage"] = "متخصص با موفقیت حذف شد.";
                Console.WriteLine($"Delete succeeded for expert ID: {id}");
            }
            return RedirectToPage();
        }
    }
}
