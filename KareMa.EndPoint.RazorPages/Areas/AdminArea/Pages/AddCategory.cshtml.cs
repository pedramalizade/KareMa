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
        [Required(ErrorMessage = " عکس نمی‌تواند بدون مقدار باشد")]
        public IFormFile Image { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {

        }
        public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
        {
            ModelState.Remove("CategoryCreate.Id"); // حذف اعتبارسنجی ID در صورت نیاز

            if (Image == null || Image.Length == 0)
            {
                ModelState.AddModelError("Image", "لطفاً یک تصویر انتخاب کنید.");
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            await _categoryAppServices.Create(CategoryCreate, Image, cancellationToken);
            return RedirectToPage("Category");
        }

        /*   public async Task<IActionResult> OnPostAddCategory(CategoryCreateDto serviceCategoryCreate, IFormFile image, CancellationToken cancellationToken)
           {
               if (ModelState.IsValid)
               {
                   await _categoryAppServices.Create(serviceCategoryCreate, image, cancellationToken);
                   return RedirectToPage("Category");
               }
               return Page();
           }*/
    }
}
