using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.Expert;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using KareMa.Infra.SqlServer.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KareMa.EndPoint.RazorPages.Pages
{
    public class ExpertDetailsModel : PageModel
    {
        private readonly ICommentAppServices _commentAppServices;
        private readonly IExpertAppServices _expertAppServices;
        private readonly AppDbContext _context;

        public ExpertDetailsModel(IExpertAppServices expertAppServices, ICommentAppServices commentAppServices, AppDbContext context)
        {
            _expertAppServices = expertAppServices;
            _commentAppServices = commentAppServices;
            _context = context;
        }

        [BindProperty]
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
            TempData["ExpertId"] = expertId;
            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Starting OnPostAddCommentAsync for ExpertId: {TempData["ExpertId"]}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
                ExpertSummary = await _expertAppServices.GetExpertSummary((int)TempData["ExpertId"], cancellationToken);
                return Page();
            }

            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userCustomerId");
                if (customerIdClaim == null || string.IsNullOrEmpty(customerIdClaim.Value))
                {
                    Console.WriteLine("CustomerId not found in claims.");
                    TempData["OrderNotDone"] = "خطا: کاربر شناسایی نشد.";
                    ExpertSummary = await _expertAppServices.GetExpertSummary((int)TempData["ExpertId"], cancellationToken);
                    return Page();
                }
                Comment.CustomerId = int.Parse(customerIdClaim.Value);
                Console.WriteLine($"CustomerId set to: {Comment.CustomerId}");

                if (TempData["ExpertId"] == null)
                {
                    Console.WriteLine("ExpertId not found in TempData.");
                    TempData["OrderNotDone"] = "خطا: متخصص شناسایی نشد.";
                    return Page();
                }
                Comment.ExpertId = (int)TempData["ExpertId"];
                Console.WriteLine($"ExpertId set to: {Comment.ExpertId}");

                var result = await _commentAppServices.Create(Comment, cancellationToken);
                Console.WriteLine($"Comment creation result: {result}");

                if (!result)
                {
                    TempData["OrderNotDone"] = "سفارش شما توسط این کارشناس به اتمام نرسیده یا خطایی رخ داده است.";
                    ExpertSummary = await _expertAppServices.GetExpertSummary(Comment.ExpertId, cancellationToken);
                    return Page();
                }

                TempData["SuccessMessage"] = "نظر شما با موفقیت ثبت شد!";
                Console.WriteLine("Comment created successfully.");
                Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                Response.Headers["Pragma"] = "no-cache";
                Response.Headers["Expires"] = "0";
                return RedirectToPage(new { expertId = Comment.ExpertId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostAddComment: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                TempData["OrderNotDone"] = $"خطا در ثبت نظر: {ex.Message}";
                ExpertSummary = await _expertAppServices.GetExpertSummary((int)TempData["ExpertId"], cancellationToken);
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
