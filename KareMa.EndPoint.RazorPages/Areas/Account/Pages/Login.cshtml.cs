using KareMa.Domain.Core.Contracts.AppService.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Account.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountAppServices _accountAppServices;

        public LoginModel(IAccountAppServices accountAppServices)
        {
            _accountAppServices = accountAppServices;
        }

        [BindProperty]
        public AccountLoginDto AccountLogin { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(AccountLoginDto accountLogin, string returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();

            var succeededLogin = await _accountAppServices.Login(accountLogin);

            if (succeededLogin)
            {
                if (returnUrl != null)
                    return LocalRedirect(returnUrl);

                if (User.IsInRole("Admin"))
                    return LocalRedirect("/AdminArea/Index");

                if (User.IsInRole("Customer"))
                    return LocalRedirect("/CustomerArea/Index");
            }

            ModelState.AddModelError(string.Empty, "??? ?????? ?? ???? ???? ?????? ???");
            return Page();
        }
    }
}
