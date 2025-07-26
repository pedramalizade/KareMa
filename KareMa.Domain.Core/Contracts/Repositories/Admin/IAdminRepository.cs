using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IAdminRepository
    {
        Task<bool> Create(AdminCreateDto adminCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int adminId, CancellationToken cancellationToken);
        Task<Admin> GetById(int adminId, CancellationToken cancellationToken);
        Task<List<Admin>> GetAll(CancellationToken cancellationToken);
        Task<AdminUpdateDto> AdminUpdateInfo(int id, CancellationToken cancellationToken);
        Task<decimal> GetAdminBalance(int adminId, CancellationToken cancellationToken);
    }

}
