namespace KareMa.EndPoint.RazorPages.Pages
{
    public class ExpertDetailsModel : PageModel
    {
        private readonly ICommentAppServices _commentAppServices;
        private readonly IExpertAppServices _expertAppServices;
        private readonly ILogger<ExpertDetailsModel> _logger;
        private readonly AppDbContext _context;

        public ExpertDetailsModel(IExpertAppServices expertAppServices, ICommentAppServices commentAppServices, AppDbContext context, ILogger<ExpertDetailsModel> logger)
        {
            _expertAppServices = expertAppServices;
            _commentAppServices = commentAppServices;
            _context = context;
            _logger = logger;
        }

        //[BindProperty]
        public ExpertSummaryDto ExpertSummary { get; set; } = new ExpertSummaryDto();

        [BindProperty]
        public CommentCreateDto Comment { get; set; } = new CommentCreateDto();

        public async Task<IActionResult> OnGetAsync(int expertId, CancellationToken cancellationToken)
        {
            Console.WriteLine($"OnGetAsync called with expertId: {expertId}");
            ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
            if (ExpertSummary == null || ExpertSummary.Id == 0)
            {
                ExpertSummary = new ExpertSummaryDto { Balance = 0, Id = expertId, Comments = new List<Comment>(), Services = new List<Service>() };
                Console.WriteLine($"Expert with ID: {expertId} not found, setting default values");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync(int expertId, CancellationToken cancellationToken)
        {
            // لاگ‌گذاری با ILogger
            ILogger<ExpertDetailsModel> logger = HttpContext.RequestServices.GetService<ILogger<ExpertDetailsModel>>();
            logger?.LogInformation("Entered OnPostAddCommentAsync with ExpertId: {ExpertId}", expertId);
            logger?.LogInformation("Raw Form Data: {FormData}", string.Join(", ", Request.Form.Select(f => $"{f.Key}={f.Value}")));
            logger?.LogInformation("Comment before setting: Title={Title}, Description={Description}, Score={Score}, ExpertId={ExpertId}, CustomerId={CustomerId}",
                Comment.Title ?? "null", Comment.Description ?? "null", Comment.Score, Comment.ExpertId, Comment.CustomerId);

            // لاگ‌گذاری با Console
            Console.WriteLine($"Entered OnPostAddCommentAsync with ExpertId: {expertId}");
            Console.WriteLine($"Raw Form Data: {string.Join(", ", Request.Form.Select(f => $"{f.Key}={f.Value}"))}");
            Console.WriteLine($"Comment before setting: Title={Comment.Title ?? "null"}, Description={Comment.Description ?? "null"}, Score={Comment.Score}, ExpertId={Comment.ExpertId}, CustomerId={Comment.CustomerId}");

            if (!ModelState.IsValid)
            {
                logger?.LogWarning("ModelState is invalid.");
                Console.WriteLine("ModelState is invalid.");
                var errors = new List<string>();
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Any())
                    {
                        var errorMessages = string.Join("; ", entry.Value.Errors.Select(e => e.ErrorMessage));
                        logger?.LogWarning("ModelState Key: {Key}, Errors: {Errors}", entry.Key, errorMessages);
                        Console.WriteLine($"ModelState Key: {entry.Key}, Errors: {errorMessages}");
                        errors.Add($"{entry.Key}: {errorMessages}");
                    }
                }
                TempData["OrderNotDone"] = string.Join(" | ", errors);
                ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                return Page();
            }

            try
            {
                // چک کردن expertId قبل از هر چیزی
                if (expertId <= 0)
                {
                    logger?.LogError("Invalid expertId received in method parameter: {ExpertId}", expertId);
                    Console.WriteLine($"Invalid expertId received in method parameter: {expertId}");
                    // تلاش برای گرفتن ExpertId از فرم
                    if (Request.Form.TryGetValue("Comment.ExpertId", out var formExpertId) && int.TryParse(formExpertId, out int formExpertIdValue) && formExpertIdValue > 0)
                    {
                        expertId = formExpertIdValue;
                        logger?.LogInformation("Overriding invalid expertId with form value: {FormExpertId}", formExpertIdValue);
                        Console.WriteLine($"Overriding invalid expertId with form value: {formExpertIdValue}");
                    }
                    else
                    {
                        logger?.LogError("No valid ExpertId found in form either.");
                        Console.WriteLine("No valid ExpertId found in form either.");
                        TempData["OrderNotDone"] = "خطا: شناسه متخصص نامعتبر است.";
                        ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                        return Page();
                    }
                }

                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userCustomerId");
                if (customerIdClaim == null || string.IsNullOrEmpty(customerIdClaim.Value))
                {
                    logger?.LogWarning("CustomerId not found in claims.");
                    Console.WriteLine("CustomerId not found in claims. Listing all claims:");
                    foreach (var claim in User.Claims)
                    {
                        Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                    }
                    TempData["OrderNotDone"] = "خطا: کاربر شناسایی نشد.";
                    ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                    return Page();
                }

                if (!int.TryParse(customerIdClaim.Value, out int customerId))
                {
                    logger?.LogWarning("Failed to parse CustomerId from claim value: {ClaimValue}", customerIdClaim.Value);
                    Console.WriteLine($"Failed to parse CustomerId from claim value: {customerIdClaim.Value}");
                    TempData["OrderNotDone"] = "خطا: شناسه مشتری نامعتبر است.";
                    ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                    return Page();
                }
                Comment.CustomerId = customerId;
                logger?.LogInformation("CustomerId set to: {CustomerId}", customerId);
                Console.WriteLine($"CustomerId set to: {customerId}");

                Comment.ExpertId = expertId;
                logger?.LogInformation("ExpertId set to: {ExpertId}", Comment.ExpertId);
                Console.WriteLine($"ExpertId set to: {Comment.ExpertId}");

                logger?.LogInformation("Calling Create with Comment: CustomerId={CustomerId}, ExpertId={ExpertId}, Title={Title}, Description={Description}, Score={Score}",
                    Comment.CustomerId, Comment.ExpertId, Comment.Title ?? "null", Comment.Description ?? "null", Comment.Score);
                Console.WriteLine($"Calling Create with Comment: CustomerId={Comment.CustomerId}, ExpertId={Comment.ExpertId}, Title={Comment.Title ?? "null"}, Description={Comment.Description ?? "null"}, Score={Comment.Score}");

                var result = await _commentAppServices.Create(Comment, cancellationToken);
                logger?.LogInformation("Create method returned: {Result}", result);
                Console.WriteLine($"Create method returned: {result}");

                if (!result)
                {
                    logger?.LogWarning("Create failed. Check Create method logs for details.");
                    Console.WriteLine("Create failed. Check Create method logs for details.");
                    TempData["OrderNotDone"] = "سفارش شما توسط این کارشناس به اتمام نرسیده یا خطایی رخ داده است.";
                    ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                    return Page();
                }

                TempData["SuccessMessage"] = "نظر شما با موفقیت ثبت شد!";
                logger?.LogInformation("Comment created successfully.");
                Console.WriteLine("Comment created successfully.");
                return RedirectToPage(new { expertId = Comment.ExpertId });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Error in OnPostAddComment");
                Console.WriteLine($"Error in OnPostAddComment: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                TempData["OrderNotDone"] = $"خطا در ثبت نظر: {ex.Message}";
                ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
                return Page();
            }
        }

        public async Task<int> ExpertAverageScores(int expertId, CancellationToken cancellationToken)
        {
            var summary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
            if (summary?.Comments == null || !summary.Comments.Any())
                return 0;
            return (int)Math.Round(summary.Comments.Average(c => c.Score), 0);
        }

        public async Task<int> ExpertOrderCount(int expertId, CancellationToken cancellationToken)
        {
            var count = await _context.Orders.CountAsync(o => o.ExpertId == expertId && o.Status == StatusEnum.Done && !o.IsDeleted, cancellationToken);
            return count;
        }
    }
}
