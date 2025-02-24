using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages.Shared
{
    [Authorize(Roles = "Customer")]
    public class CustomerProfileSettingModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;

        public CustomerProfileSettingModel(ICustomerAppServices customerAppServices, ICityAppServices cityAppService)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
        }

        [BindProperty]
        public CustomerUpdateDto CustomerUpdate { get; set; } = new CustomerUpdateDto();

        [BindProperty]
        public List<City> Cities { get; set; } = new List<City>();

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfo(userCustomerId, cancellationToken);
            Cities = await _cityAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnPostUpdate(CustomerUpdateDto customerUpdate, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
                customerUpdate.Id = userCustomerId;
                customerUpdate.Address.CustomerId = userCustomerId;
                await _customerAppServices.Update(customerUpdate, cancellationToken);
                return RedirectToAction("OnGet");
            }
            return RedirectToAction("OnGet");
        }
    }
}
