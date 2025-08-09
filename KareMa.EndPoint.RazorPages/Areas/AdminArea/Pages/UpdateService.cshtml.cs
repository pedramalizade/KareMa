namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class UpdateServiceModel : PageModel
    {
        private readonly IServiceAppServices _serviceAppServices;
        private readonly ISubCategoryAppServices _subCategoryAppServices;
        public UpdateServiceModel(IServiceAppServices serviceAppServices, ISubCategoryAppServices subCategoryAppServices)
        {
            _serviceAppServices = serviceAppServices;
            _subCategoryAppServices = subCategoryAppServices;
        }

        [BindProperty]
        public ServiceUpdateDto ServiceUpdate { get; set; }

        [BindProperty]
        public List<SubCategoryNameDto> SubCategoryNames { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            ServiceUpdate = await _serviceAppServices.ServiceUpdateInfoAsync(id, cancellationToken);
            SubCategoryNames = await _subCategoryAppServices.GetCategorisNameAsync(cancellationToken);
        }
        public async Task<IActionResult> OnPostUpdate(ServiceUpdateDto serviceUpdate, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _serviceAppServices.UpdateAsync(serviceUpdate, cancellationToken);
                return RedirectToPage("Service");
            }
            return Page();
        }
    }
}
