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
        private readonly ICustomerAppServices _customerAppServices;
        private readonly IExpertAppServices _expertAppServices;
        private readonly ILogger<CustomerOrdersModel> _logger;

        public CustomerOrdersModel(
            IOrderAppServices orderAppServices,
            ISuggestionAppServices suggestionAppServices,
            ICustomerAppServices customerAppServices,
            IExpertAppServices expertAppServices,
            ILogger<CustomerOrdersModel> logger)
        {
            _orderAppServices = orderAppServices;
            _suggestionAppServices = suggestionAppServices;
            _customerAppServices = customerAppServices;
            _expertAppServices = expertAppServices;
            _logger = logger;
        }
        private decimal CalculateAdminCommission(decimal transactionAmount)
        {
            return transactionAmount * 0.10m;
        }

        [BindProperty]
        public List<GetOrderDto> Orders { get; set; } = new List<GetOrderDto>();

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CustomerOrders OnGet started.");
            Console.WriteLine("CustomerOrders OnGet started.");

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                _logger.LogWarning("No valid customer ID found in claims.");
                Console.WriteLine("No valid customer ID found in claims.");
                return;
            }

            Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
            _logger.LogInformation("Orders loaded successfully for Customer ID: {CustomerId}", userCustomerId);
            Console.WriteLine($"Orders loaded successfully for Customer ID: {userCustomerId}");
        }

        public async Task<IActionResult> OnPostAcceptSuggestionAsync(int id, int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AcceptSuggestion started for Suggestion ID: {SuggestionId}, Order ID: {OrderId}", id, orderId);
            Console.WriteLine($"AcceptSuggestion started for Suggestion ID: {id}, Order ID: {orderId}");

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                _logger.LogWarning("No valid customer ID found in claims.");
                Console.WriteLine("No valid customer ID found in claims.");
                ModelState.AddModelError("", "کاربر معتبر نیست.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            var suggestion = await _suggestionAppServices.GetSuggestionById(id, cancellationToken);
            if (suggestion == null || suggestion.OrderId != orderId)
            {
                _logger.LogWarning("Suggestion not found or invalid for Suggestion ID: {SuggestionId}", id);
                Console.WriteLine($"Suggestion not found or invalid for Suggestion ID: {id}");
                ModelState.AddModelError("", "پیشنهاد مورد نظر یافت نشد.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            var customer = await _customerAppServices.GetCustomerById(userCustomerId, cancellationToken);
            if (customer == null)
            {
                _logger.LogWarning("Customer not found for ID: {CustomerId}", userCustomerId);
                Console.WriteLine($"Customer not found for ID: {userCustomerId}");
                ModelState.AddModelError("", "مشتری یافت نشد.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            if (customer.Balance < suggestion.Price)
            {
                _logger.LogWarning("Insufficient balance for Customer ID: {CustomerId}. Balance: {Balance}, Price: {Price}", userCustomerId, customer.Balance, suggestion.Price);
                Console.WriteLine($"Insufficient balance for Customer ID: {userCustomerId}. Balance: {customer.Balance}, Price: {suggestion.Price}");
                ModelState.AddModelError("", "موجودی شما کافی نیست.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            var result = await _suggestionAppServices.AcceptSuggestion(id, orderId, cancellationToken);
            if (!result)
            {
                _logger.LogWarning("Failed to accept suggestion for Suggestion ID: {SuggestionId}", id);
                Console.WriteLine($"Failed to accept suggestion for Suggestion ID: {id}");
                ModelState.AddModelError("", "شما برای یک سفارش فقط می‌توانید یک متخصص انتخاب کنید یا خطایی رخ داده است.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            var expert = await _expertAppServices.GetExpertById(suggestion.ExpertId, cancellationToken);
            if (expert == null)
            {
                _logger.LogWarning("Expert not found for ID: {ExpertId}", suggestion.ExpertId);
                Console.WriteLine($"Expert not found for ID: {suggestion.ExpertId}");
                ModelState.AddModelError("", "متخصص یافت نشد.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            // محاسبه 10 درصد برای ادمین
            decimal transactionAmount = suggestion.Price;
            decimal adminCommission = CalculateAdminCommission(transactionAmount);
            decimal expertAmount = transactionAmount - adminCommission;

            // فرض می‌کنیم ادمین یه حساب مشخص داره (مثلاً ID ثابت یا از سرویس)
            var adminId = 1; // اینجا باید ID واقعی ادمین رو بذاری یا از یه سرویس بگیری
            var admin = await _customerAppServices.GetCustomerById(adminId, cancellationToken); // فرضاً ادمین هم توی همین سرویس مشتری‌ها باشه
            if (admin == null)
            {
                _logger.LogWarning("Admin not found for ID: {AdminId}", adminId);
                Console.WriteLine($"Admin not found for ID: {adminId}");
                ModelState.AddModelError("", "ادمین یافت نشد.");
                Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
                return Page();
            }

            // تراکنش‌ها
            customer.Balance -= transactionAmount; // کل مبلغ از مشتری کم میشه
            expert.Balance += expertAmount;       // 90 درصد به متخصص
            admin.Balance += adminCommission;     // 10 درصد به ادمین

            // به‌روزرسانی موجودی‌ها
            await _customerAppServices.UpdateBalance(userCustomerId, customer.Balance, cancellationToken);
            await _expertAppServices.UpdateBalance(suggestion.ExpertId, expert.Balance, cancellationToken);
            await _customerAppServices.UpdateBalance(adminId, admin.Balance, cancellationToken); // فرضاً همون سرویس مشتری‌ها برای ادمین

            _logger.LogInformation("Payment completed. Customer ID: {CustomerId}, Expert ID: {ExpertId}, Admin ID: {AdminId}, Amount: {Price}, Admin Commission: {AdminCommission}",
                userCustomerId, suggestion.ExpertId, adminId, transactionAmount, adminCommission);
            Console.WriteLine($"Payment completed. Customer ID: {userCustomerId}, Expert ID: {suggestion.ExpertId}, Admin ID: {adminId}, Amount: {transactionAmount}, Admin Commission: {adminCommission}");

            TempData["SuccessMessage"] = "پیشنهاد با موفقیت تأیید و پرداخت انجام شد.";
            Orders = await _orderAppServices.GetOrders(userCustomerId, cancellationToken);
            return Page();
        }
    }
}
