namespace KareMa.Domain.AppService
{
    public class AdminAppServices : IAdminAppServices
    {
        private readonly IAdminServices _adminServices;

        public AdminAppServices(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        /// <summary>
        /// ایجاد یک ادمین جدید
        /// </summary>
        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
         => await _adminServices.CreateAsync(adminCreateDto, cancellationToken);
        /// <summary>
        /// حذف ادمین بر اساس شناسه
        /// </summary>
        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
         => await _adminServices.DeleteAsync(adminId, cancellationToken);

        /// <summary>
        /// دریافت تمام ادمین‌ها
        /// </summary>
        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
       => await _adminServices.GetAllAsync(cancellationToken);
        /// <summary>
        /// دریافت اطلاعات یک ادمین برای بروزرسانی
        /// </summary>
        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int adminId, CancellationToken cancellationToken)
          => await _adminServices.AdminUpdateInfoAsync(adminId, cancellationToken);
        /// <summary>
        /// دریافت ادمین بر اساس شناسه
        /// </summary>
        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
   => await _adminServices.GetByIdAsync(adminId, cancellationToken);
        /// <summary>
        /// بروزرسانی اطلاعات ادمین
        /// </summary>
        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
          => await _adminServices.UpdateAsync(adminUpdateDto, cancellationToken);
        /// <summary>
        /// دریافت موجودی ادمین
        /// </summary>
        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
        {
            return await _adminServices.GetAdminBalanceAsync(adminId, cancellationToken);
        }
    }
}
