using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.DTOs.ServiceDTO;
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
    public class ServiceRepository : IServiceRepository
    
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(ServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        {

            var newModel = new Service()
            {
                Name = serviceCreateDto.Name,
                SubCategoryId = serviceCreateDto.SubCategoryId,
                Image = serviceCreateDto.Image,
                Price = serviceCreateDto.Price,
            };
            await _context.Services.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<bool> Delete(int serviceId, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<GetServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            var services = await _context.Services.AsNoTracking()
                  .Select(s => new GetServiceDto
                  {
                      Id = s.Id,
                      Name = s.Name,
                      IsDeleted = s.IsDeleted,
                      Price = s.Price,
                      SubCategoryId = s.SubCategoryId,
                      SubCategory = s.SubCategory,
                      Image = s.Image,
                  })
                  .ToListAsync(cancellationToken);
            return services;
        }

        public async Task<Service> GetById(int serviceId, CancellationToken cancellationToken)
       => await FindService(serviceId, cancellationToken);

        public async Task<bool> Update(ServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindService(serviceUpdateDto.Id, cancellationToken);

            targetModel.Name = serviceUpdateDto.Name;
            targetModel.Price = serviceUpdateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task<Service> FindService(int id, CancellationToken cancellationToken)
       => await _context.Services.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
