namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages
{
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        private readonly ISuggestionAppServices _suggestionAppServices;
        private readonly ICustomerAppServices _customerAppServices;
        private readonly IExpertAppServices _expertAppServices;

        public CustomerOrdersModel(
            IOrderAppServices orderAppServices,
            ISuggestionAppServices suggestionAppServices,
            ICustomerAppServices customerAppServices,
            IExpertAppServices expertAppServices)
        {
            _orderAppServices = orderAppServices;
            _suggestionAppServices = suggestionAppServices;
            _customerAppServices = customerAppServices;
            _expertAppServices = expertAppServices;
        }

        private decimal CalculateAdminCommission(decimal transactionAmount)
        {
            return transactionAmount * 0.10m;
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
                return await ReturnWithError("کاربر معتبر نیست.", userCustomerId, cancellationToken);
            }

            var suggestion = await _suggestionAppServices.GetSuggestionByIdAsync(id, cancellationToken);
            if (suggestion == null || suggestion.OrderId != orderId)
            {
                return await ReturnWithError("پیشنهاد مورد نظر یافت نشد.", userCustomerId, cancellationToken);
            }

            var customer = await _customerAppServices.GetCustomerByIdAsync(userCustomerId, cancellationToken);
            if (customer == null)
            {
                return await ReturnWithError("مشتری یافت نشد.", userCustomerId, cancellationToken);
            }

            if (customer.Balance < suggestion.Price)
            {
                return await ReturnWithError("موجودی شما کافی نیست.", userCustomerId, cancellationToken);
            }

            if (!await _suggestionAppServices.AcceptSuggestionAsync(id, orderId, cancellationToken))
            {
                return await ReturnWithError("شما برای یک سفارش فقط می‌توانید یک متخصص انتخاب کنید یا خطایی رخ داده است.", userCustomerId, cancellationToken);
            }

            var expert = await _expertAppServices.GetExpertByIdAsync(suggestion.ExpertId, cancellationToken);
            if (expert == null)
            {
                return await ReturnWithError("متخصص یافت نشد.", userCustomerId, cancellationToken);
            }

            var adminId = 1;
            var admin = await _customerAppServices.GetCustomerByIdAsync(adminId, cancellationToken);
            if (admin == null)
            {
                return await ReturnWithError("ادمین یافت نشد.", userCustomerId, cancellationToken);
            }

            await ProcessTransaction(customer, expert, admin, suggestion.Price, userCustomerId, suggestion.ExpertId, adminId, cancellationToken);

            TempData["SuccessMessage"] = "پیشنهاد با موفقیت تأیید و پرداخت انجام شد.";
            return await ReturnWithOrders(userCustomerId, cancellationToken);
        }

        private int GetUserCustomerId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
        }

        private async Task<IActionResult> ReturnWithError(string message, int userCustomerId, CancellationToken cancellationToken)
        {
            ModelState.AddModelError("", message);
            return await ReturnWithOrders(userCustomerId, cancellationToken);
        }

        private async Task<IActionResult> ReturnWithOrders(int userCustomerId, CancellationToken cancellationToken)
        {
            Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
            return Page();
        }

        private async Task ProcessTransaction(Customer customer, Expert expert, Customer admin,
                                              decimal transactionAmount, int userCustomerId,
                                              int expertId, int adminId, CancellationToken cancellationToken)
        {
            decimal adminCommission = CalculateAdminCommission(transactionAmount);
            decimal expertAmount = transactionAmount - adminCommission;

            customer.Balance -= transactionAmount;
            expert.Balance += expertAmount;
            admin.Balance += adminCommission;

            await _customerAppServices.UpdateBalanceAsync(userCustomerId, customer.Balance, cancellationToken);
            await _expertAppServices.UpdateBalanceAsync(expertId, expert.Balance, cancellationToken);
            await _customerAppServices.UpdateBalanceAsync(adminId, admin.Balance, cancellationToken);
        }
    }
}