using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public CategoryUpdateDto ServiceCategoryUpdate { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public Category ServiceCategory { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            ServiceCategory = await _categoryAppServices.GetById(id, cancellationToken);
        }
        public async Task<IActionResult> OnPostUpdate(CategoryUpdateDto serviceCategoryUpdate, IFormFile image, CancellationToken cancellationToken)
        {
            await _categoryAppServices.Update(serviceCategoryUpdate, image, cancellationToken);
            return RedirectToPage("Category");
        }
    }
}
