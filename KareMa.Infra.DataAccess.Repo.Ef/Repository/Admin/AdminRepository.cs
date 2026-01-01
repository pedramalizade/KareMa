namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// یک ادمین جدید ایجاد می‌کند.
        /// </summary>
        /// <param name="adminCreateDto">اطلاعات ادمین.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>در صورت موفقیت مقدار true برمی‌گرداند.</returns>
        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Admin()
            {
                FirstName = adminCreateDto.FirstName,
                LastName = adminCreateDto.LastName,
                Gender = adminCreateDto.Gender,
            };

            await Queryable.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// دریافت موجودی ادمین بر اساس شناسه
        /// </summary>
        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
        {
            var admin = await Queryable.AsNoTracking().FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted, cancellationToken);
            return admin?.Balance ?? 0m;
        }

        /// <summary>
        /// حذف ادمین
        /// </summary>
        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
        {
            var targetAdmin = await FindAdmin(adminId, cancellationToken);
            targetAdmin.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// دریافت لیست همه ادمین ها
        /// </summary>
        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
       => await Queryable.AsNoTracking().ToListAsync(cancellationToken);

        /// <summary>
        /// آپدیت اطلاعات ادمین
        /// </summary>
        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            var updateInfo = await Queryable.Select(a => new AdminUpdateDto
            {
                Id = a.Id,
                Email = a.AppUser.Email,
                FirstName = a.FirstName,
                Balance = a.Balance,
                LastName = a.LastName,
                PhoneNumber = a.AppUser.PhoneNumber

            }).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            return updateInfo;
        }

        /// <summary>
        /// دریافت ادمین بر اساس شناسه
        /// </summary>
        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
          => await FindAdmin(adminId, cancellationToken);

        /// <summary>
        /// به‌روزرسانی اطلاعات ادمین
        /// </summary>
        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = Queryable.FirstOrDefault(a => a.Id == adminUpdateDto.Id);
            targetModel.FirstName = adminUpdateDto.FirstName;
            targetModel.LastName = adminUpdateDto.LastName;
            targetModel.Balance = adminUpdateDto.Balance;
            targetModel.Gender = adminUpdateDto.Gender;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// پیدا کردن ادمین بر اساس شناسه
        /// </summary>
        private async Task<Admin> FindAdmin(int id, CancellationToken cancellationToken)
      => await Queryable.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

}
