using KareMa.Domain.AppService;
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class AddSubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        private readonly ICategoryAppServices _categoryAppServices;
        public AddSubCategoryModel(ISubCategoryAppServices subCategoryAppServices, ICategoryAppServices categoryAppServices)
        {
            _subCategoryAppServices = subCategoryAppServices;
            _categoryAppServices = categoryAppServices;
        }
        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; }
        [BindProperty]
        public SubCategoryCreateDto ServiceSubCategoryCreate { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            CategoryNames = await _categoryAppServices.GetCategorisName(cancellationToken) ?? new List<CategoryNameDto>();
        }
        public async Task<IActionResult> OnPostAdd(SubCategoryCreateDto subCategoryCreate, CancellationToken cancellationToken, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _subCategoryAppServices.Create(subCategoryCreate, image, cancellationToken);
            return RedirectToPage("SubCategory");
        }
    }
}
