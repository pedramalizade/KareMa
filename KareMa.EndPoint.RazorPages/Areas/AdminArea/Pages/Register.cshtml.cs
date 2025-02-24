using KareMa.Domain.Core.Contracts.AppService.Account;
using KareMa.Domain.Core.DTOs.AccountDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{

    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly IAccountAppServices _accountAppServices;

        public RegisterModel(IAccountAppServices accountAppServices)
        {
            _accountAppServices = accountAppServices;
        }

        [BindProperty]
        public AccountAdminRegisterDto AccountRegister { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(AccountAdminRegisterDto accountRegister, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/AdminArea/Register");

            if (!ModelState.IsValid)
                return Page();

            var result = await _accountAppServices.AdminRegister(accountRegister);

            if (result.Count == 0)
            {
                TempData["Success"] = "????? ???? ?? ????? ??? ??";
                return LocalRedirect(returnUrl);
            }
            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }


    }

}
