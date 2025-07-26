using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<bool> Create(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
          =>await _adminRepository.Create(adminCreateDto, cancellationToken);
        public async Task<bool> Delete(int adminId, CancellationToken cancellationToken)
     => await _adminRepository.Delete(adminId, cancellationToken);
        public async Task<List<Admin>> GetAll(CancellationToken cancellationToken)
   => await _adminRepository.GetAll(cancellationToken);
        public async Task<AdminUpdateDto> AdminUpdateInfo(int id, CancellationToken cancellationToken)
  => await _adminRepository.AdminUpdateInfo(id, cancellationToken);
        public async Task<Admin> GetById(int adminId, CancellationToken cancellationToken)
          => await _adminRepository.GetById(adminId, cancellationToken);
        public async Task<bool> Update(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
             => await _adminRepository.Update(adminUpdateDto, cancellationToken);

        public async Task<decimal> GetAdminBalance(int adminId, CancellationToken cancellationToken)
        => await _adminRepository.GetAdminBalance(adminId, cancellationToken);  
    }
}
