namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IAdminAppServices
    {
        Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken);
        public Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken);
        public Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken);
        Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken); 

    }
}
