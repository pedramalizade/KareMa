using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    public class UpdateServiceModel : PageModel
    {
        private readonly IServiceAppServices _serviceAppServices;
        public UpdateServiceModel(IServiceAppServices serviceAppServices)
        {
            _serviceAppServices = serviceAppServices;
        }
        [BindProperty]
        public Service Service { get; set; }
        [BindProperty]
        public ServiceUpdateDto ServiceUpdate { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            Service = await _serviceAppServices.GetById(id, cancellationToken);
        }
        public async Task<IActionResult> OnPostUpdate(ServiceUpdateDto serviceUpdate, CancellationToken cancellationToken)
        {
            await _serviceAppServices.Update(serviceUpdate, cancellationToken);
            return RedirectToPage("Service");
        }
    }
}
