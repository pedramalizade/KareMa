using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.ExpertArea.Pages
{
    [Authorize(Roles = "Expert")]
    public class OpenOrdersModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;

        public OpenOrdersModel(IOrderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }

        [BindProperty]
        public List<OrdersByServiceIdsDto> Orders { get; set; }


        public async Task OnGet(CancellationToken cancellationToken)
        {
            var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId").Value);
            Orders = await _orderAppServices.GetOrdersByExpertId(expertId, cancellationToken);
        }
    }

}
