using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> Delete(int adminId, CancellationToken cancellationToken)
        {
            var targetAdmin = await FindAdmin(adminId, cancellationToken);
            targetAdmin.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<List<Admin>> GetAll(CancellationToken cancellationToken)
       => await _context.Admins.AsNoTracking().ToListAsync(cancellationToken);


        public async Task<Admin> GetById(int adminId, CancellationToken cancellationToken)
          => await FindAdmin(adminId, cancellationToken);

        public async Task<bool> Update(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = _context.Admins.FirstOrDefault(a => a.Id == adminUpdateDto.Id);
            targetModel.FirstName = adminUpdateDto.FirstName;
            targetModel.LastName = adminUpdateDto.LastName;
            targetModel.Email = adminUpdateDto.Email;
            targetModel.Gender = adminUpdateDto.Gender;
            targetModel.Password = adminUpdateDto.Password;
            targetModel.PhoneNumber = adminUpdateDto.PhoneNumber;
            _context.SaveChanges();
            return true;
        }
        private async Task<Admin> FindAdmin(int id, CancellationToken cancellationToken)
      => await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

}
