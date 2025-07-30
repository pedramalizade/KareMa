namespace KareMa.Domain.Core.Contracts.AppService.Account
{
    public interface IAccountAppServices
    {
        Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto);
        Task<bool> Login(AccountLoginDto accountLoginDto);
        Task<List<IdentityError>> AdminRegister(AccountAdminRegisterDto accountAdminRegisterDto);
    }
}
