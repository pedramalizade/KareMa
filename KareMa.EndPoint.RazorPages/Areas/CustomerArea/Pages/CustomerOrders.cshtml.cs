namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages
{
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        private readonly ICustomerOrderWorkflowAppServices _workflow;


        public CustomerOrdersModel(
            IOrderAppServices orderAppServices,
            ICustomerOrderWorkflowAppServices workflow)
        {
            _orderAppServices = orderAppServices;
            _workflow = workflow;
        }

        [BindProperty]
        public List<GetOrderDto> Orders { get; set; } = new List<GetOrderDto>();

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                return;
            }

            Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
        }

        public async Task<IActionResult> OnPostAcceptSuggestionAsync(int id, int orderId, CancellationToken cancellationToken)
        {
            var userCustomerId = GetUserCustomerId();
            if (userCustomerId == 0)
            {
                ModelState.AddModelError("", "کاربر معتبر نیست.");
                return await ReturnWithOrders(userCustomerId, cancellationToken);
            }

            var result = await _workflow.AcceptSuggestionAndProcessPaymentAsync(
                id, orderId, userCustomerId, cancellationToken);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return await ReturnWithOrders(userCustomerId, cancellationToken);
            }

            TempData["SuccessMessage"] = "پیشنهاد با موفقیت تأیید و پرداخت انجام شد.";
            return await ReturnWithOrders(userCustomerId, cancellationToken);
        }

        private int GetUserCustomerId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
        }

        private async Task<IActionResult> ReturnWithOrders(int userCustomerId, CancellationToken cancellationToken)
        {
            Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
            return Page();
        }
    }
}