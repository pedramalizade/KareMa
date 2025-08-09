namespace KareMa.EndPoint.RazorPages.Areas.ExpertArea.Pages
{
    [Authorize(Roles = "Expert")]
    public class IndexModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;

        public IndexModel(IExpertAppServices expertAppServices)
        {
            _expertAppServices = expertAppServices;
        }

        [BindProperty]
        public ExpertNameDto ExpertName { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId").Value);
            ExpertName = await _expertAppServices.GetExpertNameAsync(expertId, cancellationToken);
        }
    }
}
