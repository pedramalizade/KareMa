namespace KareMa.Domain.Core.Contracts.Repositories
{
    public interface IAddressRepository
    {
        public Task<bool> CreateAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(AddressUpdateDto addrressUpdateDto, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(int addressId, CancellationToken cancellationToken);
        public Task<Address> GetByIdAsync(int addressId, CancellationToken cancellationToken);
        public Task<List<Address>> GetAllAsync(CancellationToken cancellationToken);
    }

}
