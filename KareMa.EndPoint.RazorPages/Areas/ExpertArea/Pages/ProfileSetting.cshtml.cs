namespace KareMa.EndPoint.RazorPages.Areas.ExpertArea.Pages
{

    [Authorize(Roles = "Expert")]
    public class ProfileSettingModel : PageModel
    {
        private readonly IExpertAppServices _expertAppServices;
        private readonly IServiceAppServices _serviceAppServices;

        public ProfileSettingModel(
            IExpertAppServices expertAppServices,
            IServiceAppServices serviceAppServices)
        {
            _expertAppServices = expertAppServices;
            _serviceAppServices = serviceAppServices;
        }

        [BindProperty]
        public ExpertUpdateDto ExpertUpdate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<ServicesNameDto> ServicesNames { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ تولد نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^(\\d{4})/(\\d{2})/(\\d{2})$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd باشد.")]
        [Length(10, 10, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 10 کاراکتر باشد")]
        public string BirthDate { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var expertIdClaim = User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value;
            if (!int.TryParse(expertIdClaim, out var expertId))
                throw new UnauthorizedAccessException("شناسه کارشناس یافت نشد.");

            ExpertUpdate = await _expertAppServices.GetExpertUpdateAsync(expertId, cancellationToken);
            ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
            BirthDate = ExpertUpdate.BirthDate.ToPersianString("yyyy/MM/dd") ?? string.Empty;
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                    return Page();
                }

                var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId")?.Value ?? "0");
                ExpertUpdate.Id = expertId;

                await _expertAppServices.UpdateProfileAsync(ExpertUpdate, Image, BirthDate, cancellationToken);

                TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ServicesNames = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
                return Page();
            }
        }
    }
}
