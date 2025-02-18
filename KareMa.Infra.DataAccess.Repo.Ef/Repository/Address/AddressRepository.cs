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
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(AddressCreateDto addressCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Address()
            {
                Area = addressCreateDto.Area,
                City = addressCreateDto.City,
                PostalCode = addressCreateDto.PostalCode,
                Street = addressCreateDto.Street,
                CityId = addressCreateDto.CityId,
                Title = addressCreateDto.Title,
                IsDefault = addressCreateDto.IsDefault,
            };
            await _context.Addresses.AddAsync(newModel);

            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Delete(int addressId, CancellationToken cancellationToken)
        {
            var targetMidel = await FindAddress(addressId, cancellationToken);
            targetMidel.IsDeleted = true;

            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Address>> GetAll(CancellationToken cancellationToken)
       => await _context.Addresses.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Address> GetById(int addressId, CancellationToken cancellationToken)
      => await FindAddress(addressId, cancellationToken);

        public async Task<bool> Update(AddressUpdateDto addrressUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindAddress(addrressUpdateDto.Id, cancellationToken);

            targetModel.Area = addrressUpdateDto.Area;
            targetModel.CityId = addrressUpdateDto.CityId;
            targetModel.City = addrressUpdateDto.City;
            targetModel.Street = addrressUpdateDto.Street;
            targetModel.PostalCode = addrressUpdateDto.PostalCode;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        private async Task<Address> FindAddress(int id, CancellationToken cancellationToken)
        => await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
