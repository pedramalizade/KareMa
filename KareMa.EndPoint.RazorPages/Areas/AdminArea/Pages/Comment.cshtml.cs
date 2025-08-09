namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class CommentModel : PageModel
    {
        private readonly ICommentAppServices _commentAppServices;
        public CommentModel(ICommentAppServices commentAppServices)
        {
            _commentAppServices = commentAppServices;
        }
        [BindProperty]
        public List<GetCommentsDto> Comments { get; set; } = new List<GetCommentsDto>();
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Comments = await _commentAppServices.GetAllAsync(cancellationToken);
        }
        public async Task<IActionResult> OnGetReject(int id, CancellationToken cancellationToken)
        {
            await _commentAppServices.RejectCommentAsync(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
        public async Task<IActionResult> OnGetAccept(int id, CancellationToken cancellationToken)
        {
            await _commentAppServices.AcceptCommentAsync(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            await _commentAppServices.DeleteAsync(id, cancellationToken);
            return RedirectToAction("OnGet");
        }
    }
}
