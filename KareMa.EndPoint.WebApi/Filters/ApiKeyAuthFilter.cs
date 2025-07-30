namespace KareMa.EndPoint.WebApi.Filters
{
    public class ApiKeyAuthFilter : IAsyncActionFilter
    {
        private const string API_KEY_HEADER = "X-API-Key";
        private readonly string _validApiKey;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _validApiKey = configuration["Seq:ApiKey"];
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "API Key is missing" });
                return;
            }

            if (apiKey != _validApiKey)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "Invalid API Key" });
                return;
            }

            await next();
        }
    }
}
