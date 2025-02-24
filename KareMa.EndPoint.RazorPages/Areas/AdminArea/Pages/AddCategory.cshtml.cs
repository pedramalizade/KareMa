using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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

        public CategoryCreateDto CategoryCreate { get; set; } = new CategoryCreateDto();

        [BindProperty]
        [Required(ErrorMessage = " ??? ????????? ???? ????? ????")]
        public IFormFile Image { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {

        }

        public async Task<IActionResult> OnPostAddCategory(CategoryCreateDto serviceCategoryCreate, CancellationToken cancellationToken, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                await _categoryAppServices.Create(serviceCategoryCreate, image, cancellationToken);
                return RedirectToPage("Category");
            }
            return Page();
        }
    }
}
