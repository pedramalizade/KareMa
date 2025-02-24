using KareMa.Domain.Core.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Contracts.AppService.Account
{
    public interface IAccountAppServices
    {
        Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto);
        Task<bool> Login(AccountLoginDto accountLoginDto);
        Task<List<IdentityError>> AdminRegister(AccountAdminRegisterDto accountAdminRegisterDto);
    }
}
