using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CustomerDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        public IndexModel(ICustomerAppServices customerAppServices)
        {
            _customerAppServices = customerAppServices;
        }

        [BindProperty]
        public CustomerSummaryDto CustomerSummary { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userCustomerId").Value);
            CustomerSummary = await _customerAppServices.GetCustomerSummary(userId, cancellationToken);
        }
    }
}
