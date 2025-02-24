using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages
{
    public class SubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;

        public SubCategoryModel(ISubCategoryAppServices subCategoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
        }

        [BindProperty]
        public List<GetByCategoryIdDto> SubCategories { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            SubCategories = await _subCategoryAppServices.GetAllByCategoryId(id, cancellationToken);
        }
    }
}
