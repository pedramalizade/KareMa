using KareMa.Domain.Core.Contracts.AppService.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Account.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountAppServices _accountAppServices;
        public RegisterModel(IAccountAppServices accountAppServices)
        {
            _accountAppServices = accountAppServices;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string FisrtName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsExpert { get; set; }
            public bool IsCustomer { get; set; }
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (Input.IsExpert && Input.IsCustomer)
            {
                ModelState.AddModelError(string.Empty, "?????? ????????? ?? ????? ????? ?? ?????");
                return Page();
            }
            returnUrl ??= Url.Content("~/");
            var result = await _accountAppServices.Register(Input.FisrtName, Input.LastName, Input.Email, Input.Password,
                Input.IsExpert, Input.IsCustomer);
            if (result.Count == 0)
            {
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
