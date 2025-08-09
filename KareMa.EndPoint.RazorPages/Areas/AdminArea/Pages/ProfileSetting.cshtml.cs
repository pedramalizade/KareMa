namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{

    [Authorize(Roles = "Admin")]
    public class ProfileSettingModel : PageModel
    {
        private readonly IAdminAppServices _adminAppServices;

        public ProfileSettingModel(IAdminAppServices adminAppServices)
        {
            _adminAppServices = adminAppServices;
        }

        [BindProperty]
        public AdminUpdateDto AdminUpdate { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var adminUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "userAdminId").Value);
            AdminUpdate = await _adminAppServices.AdminUpdateInfoAsync(adminUserId, cancellationToken);
        }

        public async Task<IActionResult> OnPostUpdateAdmin(AdminUpdateDto adminUpdate, CancellationToken cancellationToken)
        {

            if (ModelState.IsValid)
            {
                var adminUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "userAdminId").Value);
                adminUpdate.Id = adminUserId;
                await _adminAppServices.UpdateAsync(adminUpdate, cancellationToken);
                return RedirectToAction("OnGet");
            }
            return RedirectToAction("OnGet");
        }
    }


}
