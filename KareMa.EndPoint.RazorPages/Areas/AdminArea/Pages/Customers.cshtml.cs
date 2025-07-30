namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppService;
        public CustomersModel(ICustomerAppServices customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [BindProperty]
        public List<GetCustomerDto> GetCustomers { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            GetCustomers = await _customerAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnPostDelete called for customer ID: {id}");
            var result = await _customerAppService.Delete(id, cancellationToken);
            if (!result)
            {
                TempData["ErrorMessage"] = "حذف مشتری با خطا مواجه شد.";
                Console.WriteLine($"Delete failed for customer ID: {id}");
            }
            else
            {
                TempData["SuccessMessage"] = "مشتری با موفقیت حذف شد.";
                Console.WriteLine($"Delete succeeded for customer ID: {id}");
            }
            return RedirectToPage();
        }
    }
}
