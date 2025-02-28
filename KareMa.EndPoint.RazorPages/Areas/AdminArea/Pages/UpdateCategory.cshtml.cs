using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
        public CategoryUpdateDto CategoryUpdate { get; set; }
        [BindProperty]
        public IFormFile? Image { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            CategoryUpdate = await _categoryAppServices.ServiceCategoryUpdateInfo(id, cancellationToken);
            // new
            if (CategoryUpdate == null)
            {
                CategoryUpdate = new CategoryUpdateDto();
            }
        }

        public async Task<IActionResult> OnPostUpdate(CategoryUpdateDto categoryUpdateDto, IFormFile? image, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)  
            {
                await _categoryAppServices.Update(categoryUpdateDto, image, cancellationToken);
                return RedirectToPage("Category");
            }
            return RedirectToPage();

        }
    }
}
