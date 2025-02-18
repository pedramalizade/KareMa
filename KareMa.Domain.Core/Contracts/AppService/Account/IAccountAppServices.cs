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
        Task<List<IdentityError>> Register(string fisrtName, string lastName, string email, string password, bool isExpert, bool isCustomer);
        Task<bool> Login(string email, string password);
    }
}
