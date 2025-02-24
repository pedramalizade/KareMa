using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.OrderDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages
{
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        private readonly ISuggestionAppServices _suggestionAppServices;

        public CustomerOrdersModel(IOrderAppServices orderAppServices, ISuggestionAppServices suggestionAppServices)
        {
            _orderAppServices = orderAppServices;
            _suggestionAppServices = suggestionAppServices;
        }

        [BindProperty]
        public List<GetOrderDto> Orders { get; set; } = new List<GetOrderDto>();

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
            Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
        }

        public async Task OnGetAcceptSuggestion(int id, int orderId, CancellationToken cancellationToken)
        {
            var result = await _suggestionAppServices.AcceptSuggestion(id, orderId, cancellationToken);

            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "??? ???? ?? ????? ??? ???????? ?? ????? ?????? ????");
            }
        }
    }
}
