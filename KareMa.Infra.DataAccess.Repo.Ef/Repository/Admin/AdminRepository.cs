using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
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

        public async Task<decimal> GetAdminBalance(int adminId, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted, cancellationToken);
            return admin?.Balance ?? 0m; // اگر ادمین پیدا نشد یا موجودی نداشت، صفر برمی‌گردونه
        }

        public async Task<bool> Delete(int adminId, CancellationToken cancellationToken)
        {
            var targetAdmin = await FindAdmin(adminId, cancellationToken);
            targetAdmin.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<List<Admin>> GetAll(CancellationToken cancellationToken)
       => await _context.Admins.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<AdminUpdateDto> AdminUpdateInfo(int id, CancellationToken cancellationToken)
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

        public async Task<Admin> GetById(int adminId, CancellationToken cancellationToken)
          => await FindAdmin(adminId, cancellationToken);

        public async Task<bool> Update(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
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
