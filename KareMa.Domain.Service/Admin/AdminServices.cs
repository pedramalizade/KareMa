namespace KareMa.Domain.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        public AdminServices(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        /// <summary>ایجاد ادمین جدید</summary>
        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
            => await _adminRepository.CreateAsync(adminCreateDto, cancellationToken);

        /// <summary>حذف ادمین</summary>
        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
            => await _adminRepository.DeleteAsync(adminId, cancellationToken);

        /// <summary>دریافت همه ادمین‌ها</summary>
        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
            => await _adminRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت اطلاعات برای ویرایش</summary>
        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int adminId, CancellationToken cancellationToken)
            => await _adminRepository.AdminUpdateInfoAsync(adminId, cancellationToken);

        /// <summary>دریافت ادمین با شناسه</summary>
        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
            => await _adminRepository.GetByIdAsync(adminId, cancellationToken);

        /// <summary>ویرایش ادمین</summary>
        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
            => await _adminRepository.UpdateAsync(adminUpdateDto, cancellationToken);

        /// <summary>دریافت موجودی ادمین</summary>
        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
            => await _adminRepository.GetAdminBalanceAsync(adminId, cancellationToken);
    }
}
