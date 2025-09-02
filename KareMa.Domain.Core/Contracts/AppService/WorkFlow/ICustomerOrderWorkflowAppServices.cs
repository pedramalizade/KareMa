namespace KareMa.Domain.Core.Contracts.AppService.WorkFlow
{
    public interface ICustomerOrderWorkflowAppServices
    {
        Task<OperationResult> AcceptSuggestionAndProcessPaymentAsync(
            int suggestionId,
            int orderId,
            int userCustomerId,
            CancellationToken cancellationToken);
    }
}
