namespace KareMa.Domain.Core.Contracts.AppService.Account
{
    public interface IAccountAppServices
    {
        /// <summary>ثبت‌نام کاربر.</summary>
        Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto);

        /// <summary>ورود کاربر.</summary>
        Task<bool> Login(AccountLoginDto accountLoginDto);

        /// <summary>ثبت‌نام ادمین.</summary>
        Task<List<IdentityError>> AdminRegister(AccountAdminRegisterDto accountAdminRegisterDto);

        /// <summary>دریافت نقش‌های کاربر با ایمیل.</summary>
        Task<IList<string>> GetUserRolesByEmail(string email);
    }
}
