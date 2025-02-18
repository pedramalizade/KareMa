using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class ChangeStatusModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        public ChangeStatusModel(IOrderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }
        [BindProperty]
        public StatusEnum Status { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            Order = await _orderAppServices.GetById(id, cancellationToken);
        }
        public async Task<IActionResult> OnPostChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            await _orderAppServices.ChangeStatus(status, orderId, cancellationToken);
            return RedirectToPage("Order");
        }
    }
}
