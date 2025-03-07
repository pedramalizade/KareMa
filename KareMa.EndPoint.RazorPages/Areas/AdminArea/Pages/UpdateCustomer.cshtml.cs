using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class UpdateCustomerModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;

        public UpdateCustomerModel(ICustomerAppServices customerAppServices)
        {
            _customerAppServices = customerAppServices;
        }

        [BindProperty]
        public CustomerUpdateDto CustomerUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnGetAsync called with id: {id}");
            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfo(id, cancellationToken);
            if (CustomerUpdate == null)
            {
                Console.WriteLine($"Customer with ID: {id} not found, redirecting to Customers.");
                TempData["ErrorMessage"] = "مشتری پیدا نشد.";
                return RedirectToPage("Customers");
            }
            Console.WriteLine($"Loaded customer with ID: {CustomerUpdate.Id}, Name: {CustomerUpdate.FirstName} {CustomerUpdate.LastName}");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnPostUpdateAsync called with Customer ID: {CustomerUpdate.Id}");
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
                var result = await _customerAppServices.Update(CustomerUpdate, Image, cancellationToken);
                if (result)
                {
                    TempData["SuccessMessage"] = "اطلاعات مشتری با موفقیت آپدیت شد.";
                    return RedirectToPage("Customers");
                }
                else
                {
                    TempData["ErrorMessage"] = "خطایی در آپدیت اطلاعات رخ داد.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostUpdate: {ex.Message}");
                TempData["ErrorMessage"] = $"خطا: {ex.Message}";
                return Page();
            }
        }
    }
}
