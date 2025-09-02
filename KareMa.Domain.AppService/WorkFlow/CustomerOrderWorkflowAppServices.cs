namespace KareMa.Domain.AppService.WorkFlow
{
    public class CustomerOrderWorkflowAppServices : ICustomerOrderWorkflowAppServices
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly IExpertAppServices _expertAppServices;
        private readonly ISuggestionAppServices _suggestionAppServices;

        public CustomerOrderWorkflowAppServices(
            ICustomerAppServices customerAppServices,
            IExpertAppServices expertAppServices,
            ISuggestionAppServices suggestionAppServices)
        {
            _customerAppServices = customerAppServices;
            _expertAppServices = expertAppServices;
            _suggestionAppServices = suggestionAppServices;
        }

        public async Task<OperationResult> AcceptSuggestionAndProcessPaymentAsync(
            int suggestionId,
            int orderId,
            int userCustomerId,
            CancellationToken cancellationToken)
        {
            var suggestion = await _suggestionAppServices.GetSuggestionByIdAsync(suggestionId, cancellationToken);
            if (suggestion == null || suggestion.OrderId != orderId)
                return OperationResult.Fail("پیشنهاد مورد نظر یافت نشد.");

            var customer = await _customerAppServices.GetCustomerByIdAsync(userCustomerId, cancellationToken);
            if (customer == null)
                return OperationResult.Fail("مشتری یافت نشد.");

            if (customer.Balance < suggestion.Price)
                return OperationResult.Fail("موجودی شما کافی نیست.");

            var acceptResult = await _suggestionAppServices.AcceptSuggestionAsync(suggestionId, orderId, cancellationToken);
            if (!acceptResult)
                return OperationResult.Fail("شما برای یک سفارش فقط می‌توانید یک متخصص انتخاب کنید یا خطایی رخ داده است.");

            var expert = await _expertAppServices.GetExpertByIdAsync(suggestion.ExpertId, cancellationToken);
            if (expert == null)
                return OperationResult.Fail("متخصص یافت نشد.");

            var adminId = 1;
            var admin = await _customerAppServices.GetCustomerByIdAsync(adminId, cancellationToken);
            if (admin == null)
                return OperationResult.Fail("ادمین یافت نشد.");

            decimal adminCommission = suggestion.Price * 0.10m;
            decimal expertAmount = suggestion.Price - adminCommission;

            customer.Balance -= suggestion.Price;
            expert.Balance += expertAmount;
            admin.Balance += adminCommission;

            await _customerAppServices.UpdateBalanceAsync(userCustomerId, customer.Balance, cancellationToken);
            await _expertAppServices.UpdateBalanceAsync(expert.Id, expert.Balance, cancellationToken);
            await _customerAppServices.UpdateBalanceAsync(adminId, admin.Balance, cancellationToken);

            return OperationResult.SuccessResult();
        }
    }
}
