using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class AddCategoryModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;
        public AddCategoryModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }
        [BindProperty]
        public CategoryCreateDto ServiceCategoryCreate { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {

        }
        public async Task<IActionResult> OnPostAdd(CategoryCreateDto categoryCreate, CancellationToken cancellationToken, IFormFile image)
        {
            await _categoryAppServices.Create(categoryCreate, image, cancellationToken);
            return RedirectToPage("Category");
        }
    }
}
