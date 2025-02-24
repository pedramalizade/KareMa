using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.Expert;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages
{
    public class ExpertDetailsModel : PageModel
    {
        private readonly ICommentAppServices _commentAppServices;
        private readonly IExpertAppServices _expertAppServices;

        public ExpertDetailsModel(IExpertAppServices expertAppServices, ICommentAppServices commentAppServices)
        {
            _expertAppServices = expertAppServices;
            _commentAppServices = commentAppServices;
        }

        [BindProperty]
        public ExpertSummaryDto ExpertSummary { get; set; } = new ExpertSummaryDto();

        [BindProperty]
        public CommentCreateDto Comment { get; set; }

        public async Task OnGet(int expertId, CancellationToken cancellationToken)
        {
            ExpertSummary = await _expertAppServices.GetExpertSummary(expertId, cancellationToken);
            TempData["ExpertId"] = expertId;
        }

        public async Task<IActionResult> OnPostAddComment(CommentCreateDto comment, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                comment.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
                comment.ExpertId = (int)TempData["ExpertId"];

                var result = await _commentAppServices.Create(comment, cancellationToken);


                if (result == false)
                {
                    TempData["OrderNotDone"] = "????? ??? ???? ??? ??????? ?? ????? ??????...?? ?? ????? ??? ????? ???? ??????? ???????? ??? ??? ?? ??? ????";
                    return RedirectToAction("OnGet", new { expertId = (int)TempData["ExpertId"] });
                }
                else
                {
                    return RedirectToAction("OnGet", new { expertId = (int)TempData["ExpertId"] });
                }

            }
            return Page();
        }
    }
}
