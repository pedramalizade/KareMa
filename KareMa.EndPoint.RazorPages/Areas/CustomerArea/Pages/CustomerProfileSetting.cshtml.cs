namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages.Shared
{
    [Authorize(Roles = "Customer")]
    public class CustomerProfileSettingModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityAppService;

        public CustomerProfileSettingModel(
            ICustomerAppServices customerAppServices,
            ICityAppServices cityAppService)
        {
            _customerAppServices = customerAppServices;
            _cityAppService = cityAppService;
        }

        [BindProperty]
        public CustomerUpdateDto CustomerUpdate { get; set; } = new CustomerUpdateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public List<City> Cities { get; set; } = new List<City>();

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0) return Unauthorized();

            CustomerUpdate = await _customerAppServices.GetCustomerUpdateInfoAsync(userCustomerId, cancellationToken);
            if (CustomerUpdate == null) return NotFound();

            Cities = await _cityAppService.GetAllAsync(cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
            {
                ModelState.AddModelError("", "کاربر معتبر نیست.");
                return await ReturnWithCities(cancellationToken);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "اطلاعات وارد شده معتبر نیست.");
                return await ReturnWithCities(cancellationToken);
            }

            var result = await _customerAppServices.UpdateProfileAsync(userCustomerId, CustomerUpdate, Image, cancellationToken);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return await ReturnWithCities(cancellationToken);
            }

            TempData["SuccessMessage"] = "تغییرات با موفقیت ذخیره شد.";
            return RedirectToPage();
        }

        private async Task<IActionResult> ReturnWithCities(CancellationToken cancellationToken)
        {
            Cities = await _cityAppService.GetAllAsync(cancellationToken);
            return Page();
        }
    }
}