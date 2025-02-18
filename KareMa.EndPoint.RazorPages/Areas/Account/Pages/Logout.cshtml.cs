using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Account.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        public LogoutModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
