using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        public OrderModel(IOrderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }
        [BindProperty]
        public List<GetOrderDto> Orders { get; set; }
        [BindProperty]
        public StatusEnum Status { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Orders = await _orderAppServices.GetAll(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _orderAppServices.Delete(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
