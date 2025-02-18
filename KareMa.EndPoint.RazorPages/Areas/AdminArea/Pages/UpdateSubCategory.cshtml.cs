using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class UpdateSubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        public UpdateSubCategoryModel(ISubCategoryAppServices subCategoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
        }
        [BindProperty]
        public SubCategoryUpdateDto SubCategoryUpdate { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public SubCategory SubCategory { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            SubCategory = await _subCategoryAppServices.GetById(id, cancellationToken);
        }
        public async Task<IActionResult> OnPostUpdate(SubCategoryUpdateDto serviceSubCategoryUpdate, IFormFile image, CancellationToken cancellationToken)
        {
            await _subCategoryAppServices.Update(serviceSubCategoryUpdate, image, cancellationToken);
            return RedirectToPage("SubCategory");
        }
    }
}
