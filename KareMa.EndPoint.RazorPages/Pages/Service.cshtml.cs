using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages
{
    public class ServiceModel : PageModel
    {
        private readonly IServiceAppServices _serviceAppServices;

        public ServiceModel(IServiceAppServices serviceAppServices)
        {
            _serviceAppServices = serviceAppServices;
        }

        [BindProperty]
        public List<GetByCategorySubIdDto> Services { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            Services = await _serviceAppServices.GetAllBySubCategoryId(id, cancellationToken);
        }
    }
}
