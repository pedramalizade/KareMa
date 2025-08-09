namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class ServiceModel : PageModel
    {
        private readonly IServiceAppServices _serviceAppServices;
        public ServiceModel(IServiceAppServices serviceAppServices)
        {
            _serviceAppServices = serviceAppServices;
        }
        [BindProperty]
        public List<GetServiceDto> Services { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Services = await _serviceAppServices.GetAllAsync(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _serviceAppServices.DeleteAsync(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
