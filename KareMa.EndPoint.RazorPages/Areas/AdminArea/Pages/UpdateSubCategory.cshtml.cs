using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class UpdateSubCategoryModel : PageModel
    {
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        private readonly ICategoryAppServices _categoryAppService;

        public UpdateSubCategoryModel(ISubCategoryAppServices subCategoryAppServices, ICategoryAppServices categoryAppService)
        {
            _subCategoryAppServices = subCategoryAppServices;
            _categoryAppService = categoryAppService;
        }

        [BindProperty]
        public SubCategoryUpdateDto SubCategoryUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; } = new List<CategoryNameDto>();

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) 
            {
                return BadRequest("Invalid Category Id");
            }

            SubCategoryUpdate = await _subCategoryAppServices.ServiceSubCategoryUpdateInfo(id, cancellationToken);

            if (SubCategoryUpdate == null)
            {
                return NotFound("SubCategory not found");
            }

            CategoryNames = await _categoryAppService.GetCategorisName(cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdate(CancellationToken cancellationToken) 
        {
            if (!ModelState.IsValid)
            {
                CategoryNames = await _categoryAppService.GetCategorisName(cancellationToken); 
                return Page();
            }

            try
            {
                await _subCategoryAppServices.Update(SubCategoryUpdate, Image, cancellationToken);
                return RedirectToPage("SubCategory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to update subcategory: {ex.Message}");
                CategoryNames = await _categoryAppService.GetCategorisName(cancellationToken);
                return Page();
            }
        }
    }
}
