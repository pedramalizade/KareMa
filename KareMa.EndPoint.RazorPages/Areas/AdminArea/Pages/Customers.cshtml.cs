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
            GetCustomers = await _customerAppService.GetAllAsync(cancellationToken);
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            var result = await _customerAppService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                TempData["ErrorMessage"] = "حذف مشتری با خطا مواجه شد.";
            }
            else
            {
                TempData["SuccessMessage"] = "مشتری با موفقیت حذف شد.";
            }
            return RedirectToPage();
        }
    }
}
