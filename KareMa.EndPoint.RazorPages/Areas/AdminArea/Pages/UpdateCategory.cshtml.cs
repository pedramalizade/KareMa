using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
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
        public CategoryUpdateDto CategoryUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnGetAsync called with id: {id}");
            CategoryUpdate = await _categoryAppServices.ServiceCategoryUpdateInfo(id, cancellationToken);
            if (CategoryUpdate == null)
            {
                Console.WriteLine($"Category with ID: {id} not found.");
                TempData["ErrorMessage"] = "دسته‌بندی پیدا نشد.";
                return RedirectToPage("Category");
            }
            Console.WriteLine($"Loaded category with ID: {CategoryUpdate.Id}, Name: {CategoryUpdate.Name}");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnPostUpdateAsync called with Category ID: {CategoryUpdate.Id}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }

            try
            {
                var result = await _categoryAppServices.Update(CategoryUpdate, Image, cancellationToken);
                if (result)
                {
                    TempData["SuccessMessage"] = "دسته‌بندی با موفقیت آپدیت شد.";
                   
                    Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                    Response.Headers["Pragma"] = "no-cache";
                    Response.Headers["Expires"] = "0";
                    return RedirectToPage("Category", new { refresh = DateTime.Now.Ticks });
                }
                else
                {
                    TempData["ErrorMessage"] = "خطایی در آپدیت دسته‌بندی رخ داد.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostUpdate: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                TempData["ErrorMessage"] = $"خطا: {ex.Message}";
                return Page();
            }
        }
    }
}
