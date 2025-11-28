namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface IAdminAppServices
    {
        /// <summary>
        /// ایجاد ادمین جدید.
        /// </summary>
        Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی اطلاعات ادمین.
        /// </summary>
        Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// حذف ادمین بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت ادمین بر اساس شناسه.
        /// </summary>
        public Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت لیست همه ادمین‌ها.
        /// </summary>
        public Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// دریافت اطلاعات بروزرسانی ادمین.
        /// </summary>
        Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت موجودی ادمین.
        /// </summary>
        Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken);

    }
}
