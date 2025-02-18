using KareMa.Domain.Core.Contracts.AppService.Account;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.AppService.Account
{
    public class AccountAppServices : IAccountAppServices
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountAppServices(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<List<IdentityError>> Register(string fisrtName, string lastName, string email, string password, bool isExpert, bool isCustomer)
        {
            var role = string.Empty;
            var user = CreateUser();
            user.UserName = email;
            user.Email = email;

            if (isCustomer)
            {
                role = "Customer";
                user.Customer = new Customer()
                {
                    FirstName = fisrtName,
                    LastName = lastName,
                };
            }
            if (isExpert)
            {
                role = "Expert";
                user.Expert = new Expert()
                {
                    FirstName = fisrtName,
                    LastName = lastName,
                };
            }
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role);
            return (List<IdentityError>)result.Errors;
        }
        public async Task<bool> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);
            return result.Succeeded;
        }
        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                                                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
