namespace KareMa.Domain.Core.Entities
{
    public class OperationResult
    {
        public bool Success { get; }
        public string ErrorMessage { get; }

        private OperationResult(bool success, string errorMessage = "")
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static OperationResult SuccessResult()
            => new OperationResult(true);

        public static OperationResult Fail(string message)
            => new OperationResult(false, message);
    }
}
