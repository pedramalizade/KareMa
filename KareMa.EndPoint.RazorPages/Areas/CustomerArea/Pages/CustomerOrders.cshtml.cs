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

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                ModelState.AddModelError("", "کاربر معتبر نیست.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            var suggestion = await _suggestionAppServices.GetSuggestionByIdAsync(id, cancellationToken);
            if (suggestion == null || suggestion.OrderId != orderId)
            {
                ModelState.AddModelError("", "پیشنهاد مورد نظر یافت نشد.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            var customer = await _customerAppServices.GetCustomerByIdAsync(userCustomerId, cancellationToken);
            if (customer == null)
            {
                ModelState.AddModelError("", "مشتری یافت نشد.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            if (customer.Balance < suggestion.Price)
            {
                ModelState.AddModelError("", "موجودی شما کافی نیست.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            var result = await _suggestionAppServices.AcceptSuggestionAsync(id, orderId, cancellationToken);
            if (!result)
            {
                ModelState.AddModelError("", "شما برای یک سفارش فقط می‌توانید یک متخصص انتخاب کنید یا خطایی رخ داده است.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            var expert = await _expertAppServices.GetExpertByIdAsync(suggestion.ExpertId, cancellationToken);
            if (expert == null)
            {
                ModelState.AddModelError("", "متخصص یافت نشد.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            decimal transactionAmount = suggestion.Price;
            decimal adminCommission = CalculateAdminCommission(transactionAmount);
            decimal expertAmount = transactionAmount - adminCommission;

            var adminId = 1;
            var admin = await _customerAppServices.GetCustomerByIdAsync(adminId, cancellationToken);
            if (admin == null)
            {
                ModelState.AddModelError("", "ادمین یافت نشد.");
                Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
                return Page();
            }

            customer.Balance -= transactionAmount; 
            expert.Balance += expertAmount;       
            admin.Balance += adminCommission;     

            await _customerAppServices.UpdateBalanceAsync(userCustomerId, customer.Balance, cancellationToken);
            await _expertAppServices.UpdateBalanceAsync(suggestion.ExpertId, expert.Balance, cancellationToken);
            await _customerAppServices.UpdateBalanceAsync(adminId, admin.Balance, cancellationToken);


            TempData["SuccessMessage"] = "پیشنهاد با موفقیت تأیید و پرداخت انجام شد.";
            Orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
            return Page();
        }
    }
}