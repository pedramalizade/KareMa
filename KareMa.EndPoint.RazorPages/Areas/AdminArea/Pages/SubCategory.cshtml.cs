using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            SubCategories = await _subCategoryAppServices.GetSubCategories(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _subCategoryAppServices.Delete(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
