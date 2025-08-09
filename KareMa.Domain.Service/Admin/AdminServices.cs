namespace KareMa.Domain.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
          =>await _adminRepository.CreateAsync(adminCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
     => await _adminRepository.DeleteAsync(adminId, cancellationToken);
        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
   => await _adminRepository.GetAllAsync(cancellationToken);
        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken)
  => await _adminRepository.AdminUpdateInfoAsync(id, cancellationToken);
        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
          => await _adminRepository.GetByIdAsync(adminId, cancellationToken);
        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
             => await _adminRepository.UpdateAsync(adminUpdateDto, cancellationToken);
        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
        => await _adminRepository.GetAdminBalanceAsync(adminId, cancellationToken);  
    }
}
