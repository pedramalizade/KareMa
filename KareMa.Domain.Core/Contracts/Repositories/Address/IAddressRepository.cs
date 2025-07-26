using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IAddressRepository
    {
        public Task<bool> Create(AddressCreateDto addressCreateDto, CancellationToken cancellationToken);
        public Task<bool> Update(AddressUpdateDto addrressUpdateDto, CancellationToken cancellationToken);
        public Task<bool> Delete(int addressId, CancellationToken cancellationToken);
        public Task<Address> GetById(int addressId, CancellationToken cancellationToken);
        public Task<List<Address>> GetAll(CancellationToken cancellationToken);
    }

}
