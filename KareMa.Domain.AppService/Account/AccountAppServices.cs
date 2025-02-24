using KareMa.Domain.Core.Contracts.AppService.Account;
using KareMa.Domain.Core.DTOs.AccountDto;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto)
        {
            var role = string.Empty;

            var user = CreateUser();

            user.UserName = accountRegisterDto.Email;
            user.Email = accountRegisterDto.Email;

            if (accountRegisterDto.isExpert)
            {
                role = "Expert";
                user.Expert = new Expert()
                {
                    FirstName = accountRegisterDto.FirstName,
                    LastName = accountRegisterDto.LastName,
                    Gender = accountRegisterDto.Gender
                };
            }
            else
            {
                role = "Customer";
                user.Customer = new Customer()
                {
                    FirstName = accountRegisterDto.FirstName,
                    LastName = accountRegisterDto.LastName,
                    Gender = accountRegisterDto.Gender
                };
            }

            var result = await _userManager.CreateAsync(user, accountRegisterDto.Password);

            if (accountRegisterDto.isExpert)
            {

                var userExpertId = user.Expert!.Id;
                await _userManager.AddClaimAsync(user, new Claim("userExpertId", userExpertId.ToString()));
            }
            else
            {

                var userCustomerId = user.Customer!.Id;
                await _userManager.AddClaimAsync(user, new Claim("userCustomerId", userCustomerId.ToString()));
            }

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role);

            return (List<IdentityError>)result.Errors;
        }

        public async Task<bool> Login(AccountLoginDto accountLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(accountLoginDto.Email, accountLoginDto.Password, false, lockoutOnFailure: false);
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

        public async Task<List<IdentityError>> AdminRegister(AccountAdminRegisterDto accountAdminRegisterDto)
        {
            var user = CreateUser();

            user.UserName = accountAdminRegisterDto.Email;
            user.Email = accountAdminRegisterDto.Email;

            user.Admin = new Admin()
            {
                FirstName = accountAdminRegisterDto.FirstName,
                LastName = accountAdminRegisterDto.LastName,
                Gender = accountAdminRegisterDto.Gender,
            };

            var result = await _userManager.CreateAsync(user, accountAdminRegisterDto.Password);

            var userAdminId = user.Admin!.Id;
            await _userManager.AddClaimAsync(user, new Claim("userAdminId", userAdminId.ToString()));

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Admin");

            return (List<IdentityError>)result.Errors;
        }
    }
}
