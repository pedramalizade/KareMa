using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using KareMa.Infra.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _context;
        public ExpertRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Expert()
            {
                FirstName = expertCreateDto.FirstName,
                LastName = expertCreateDto.LastName,
                Gender = expertCreateDto.Gender,
                PhoneNumber = expertCreateDto.PhoneNumber,
                Address = expertCreateDto.Address,
                BirthDate = expertCreateDto.BirthDate,
                ProfileImage = expertCreateDto.ProfileImage,
                Services = expertCreateDto.Services,
            };
            await _context.Experts.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
        {
            var targetModel = await FindExpert(expertId, cancellationToken);
            targetModel.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Experts.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
        {
            return await FindExpert(expertId, cancellationToken);
        }

        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Experts
                .Include(e => e.Services)
                .FirstOrDefaultAsync(e => e.Id == expertUpdateDto.Id, cancellationToken);

            if (targetModel != null)
            {
                targetModel.Services ??= new List<Service>();

                targetModel.Services.Clear();

                if (expertUpdateDto.ServiceIds is not null)
                {
                    foreach (var item in expertUpdateDto.ServiceIds)
                    {
                        var service = await _context.Services
                            .FirstOrDefaultAsync(c => c.Id == item, cancellationToken);

                        targetModel.Services.Add(service);
                    }
                }
                targetModel.FirstName = expertUpdateDto.FirstName;
                targetModel.LastName = expertUpdateDto.LastName;
                targetModel.PhoneNumber = expertUpdateDto.PhoneNumber;
                targetModel.BirthDate = expertUpdateDto.BirthDate;

                if (expertUpdateDto.ProfileImage != null)
                {
                    targetModel.ProfileImage = expertUpdateDto.ProfileImage;
                }

            }
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<Expert> FindExpert(int id, CancellationToken cancellationToken)
   => await _context.Experts.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
