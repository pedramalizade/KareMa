using KareMa.Domain.AppService;
using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using KareMa.Domain.Core.DTOs.SubCategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class AddServiceModel : PageModel
    {

        private readonly IServiceAppServices _serviceAppServices;
        private readonly ISubCategoryAppServices _subCategoryAppServices;

        public AddServiceModel(IServiceAppServices serviceAppServices, ISubCategoryAppServices subCategoryAppServices)
        {
            _serviceAppServices = serviceAppServices;
            _subCategoryAppServices = subCategoryAppServices;
        }
        [BindProperty]
        public List<SubCategoryNameDto> SubCategoryNames { get; set; }

        [BindProperty]
        public ServiceCreateDto ServiceCreate { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            SubCategoryNames = await _subCategoryAppServices.GetCategorisName(cancellationToken);
        }
        public async Task<IActionResult> OnPostAddService(ServiceCreateDto serviceCreate, CancellationToken cancellationToken, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                await _serviceAppServices.Create(serviceCreate, cancellationToken, image);
                return RedirectToPage("Service");
            }
            return Page();
        }
    }
}
