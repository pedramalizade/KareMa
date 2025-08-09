namespace KareMa.Domain.Core.Contracts.Service
{
    public interface IAdminServices
    {
        Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken);
        Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken);
        Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken);
        Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken);
        Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken); 
    }
}
