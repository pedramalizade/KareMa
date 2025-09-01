namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Admin()
            {
                FirstName = adminCreateDto.FirstName,
                LastName = adminCreateDto.LastName,
                Gender = adminCreateDto.Gender,
            };

            await _context.Admins.AddAsync(newModel, cancellationToken);
            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<decimal> GetAdminBalanceAsync(int adminId, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted, cancellationToken);
            return admin?.Balance ?? 0m; 
        }

        public async Task<bool> DeleteAsync(int adminId, CancellationToken cancellationToken)
        {
            var targetAdmin = await FindAdmin(adminId, cancellationToken);
            targetAdmin.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken)
       => await _context.Admins.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<AdminUpdateDto> AdminUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            var m = await _context.Admins.Select(a => new AdminUpdateDto
            {
                Id = a.Id,
                Email = a.AppUser.Email,
                FirstName = a.FirstName,
                Balance = a.Balance,
                LastName = a.LastName,
                PhoneNumber = a.AppUser.PhoneNumber

            }).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            return m;
        }

        public async Task<Admin> GetByIdAsync(int adminId, CancellationToken cancellationToken)
          => await FindAdmin(adminId, cancellationToken);

        public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = _context.Admins.FirstOrDefault(a => a.Id == adminUpdateDto.Id);
            targetModel.FirstName = adminUpdateDto.FirstName;
            targetModel.LastName = adminUpdateDto.LastName;
            targetModel.Balance = adminUpdateDto.Balance;
            targetModel.Gender = adminUpdateDto.Gender;
            _context.SaveChanges();
            return true;
        }
        private async Task<Admin> FindAdmin(int id, CancellationToken cancellationToken)
      => await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

}
