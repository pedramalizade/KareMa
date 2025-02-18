using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;
        public CategoryModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }
        [BindProperty]
        public List<GetCategoryDto> GetCategories { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            GetCategories = await _categoryAppServices.GetAll(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _categoryAppServices.Delete(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
