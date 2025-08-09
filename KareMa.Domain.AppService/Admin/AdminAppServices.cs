namespace KareMa.Domain.AppService
{
    public class AdminAppServices : IAdminAppServices
    {
        private readonly IAdminServices _adminServices;

        public AdminAppServices(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
         => await _adminServices.CreateAsync(adminCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
         => await _adminServices.DeleteAsync(adminId, cancellationToken);
        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
       => await _adminServices.GetAllAsync(cancellationToken);
        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken)
          => await _adminServices.AdminUpdateInfoAsync(id, cancellationToken);
        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
   => await _adminServices.GetByIdAsync(adminId, cancellationToken);
        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
          => await _adminServices.UpdateAsync(adminUpdateDto, cancellationToken);

        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
        {
            return await _adminServices.GetAdminBalanceAsync(adminId, cancellationToken);
        }
    }
}
