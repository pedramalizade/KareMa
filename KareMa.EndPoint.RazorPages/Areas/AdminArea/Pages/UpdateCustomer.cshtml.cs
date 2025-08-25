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
            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfoAsync(id, cancellationToken);
            if (CustomerUpdate == null)
            {
                TempData["ErrorMessage"] = "مشتری پیدا نشد.";
                return RedirectToPage("Customers");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }

            try
            {
                var result = await _customerAppServices.UpdateAsync(CustomerUpdate, Image, cancellationToken);
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
                TempData["ErrorMessage"] = $"خطا: {ex.Message}";
                return Page();
            }
        }
    }
}
