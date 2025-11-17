namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// یک آدرس جدید ایجاد می‌کند.
        /// </summary>
        /// <param name="addressCreateDto">اطلاعات آدرس.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>در صورت موفقیت مقدار true برمی‌گرداند.</returns>
        public async Task<bool> CreateAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken)
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

        /// <summary>
        /// یک آدرس را به‌صورت نرم‌حذف (Soft Delete) غیرفعال می‌کند.
        /// </summary>
        /// <param name="addressId">شناسه آدرس.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>در صورت موفقیت مقدار true برمی‌گرداند.</returns>
        public async Task<bool> DeleteAsync(int addressId, CancellationToken cancellationToken)
        {
            var targetMidel = await FindAddress(addressId, cancellationToken);
            targetMidel.IsDeleted = true;

            _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// تمام آدرس‌ها را بازیابی می‌کند.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست آدرس‌ها.</returns>
        public async Task<List<Address>> GetAllAsync(CancellationToken cancellationToken)
       => await _context.Addresses.AsNoTracking().ToListAsync(cancellationToken);

        /// <summary>
        /// آدرس را بر اساس شناسه برمی‌گرداند.
        /// </summary>
        /// <param name="addressId">شناسه آدرس.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>آدرس مورد نظر.</returns>
        public async Task<Address> GetByIdAsync(int addressId, CancellationToken cancellationToken)
      => await FindAddress(addressId, cancellationToken);

        /// <summary>
        /// اطلاعات یک آدرس را به‌روزرسانی می‌کند.
        /// </summary>
        /// <param name="addrressUpdateDto">اطلاعات جدید آدرس.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>در صورت موفقیت مقدار true برمی‌گرداند.</returns>
        public async Task<bool> UpdateAsync(AddressUpdateDto addrressUpdateDto, CancellationToken cancellationToken)
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

        /// <summary>
        /// آدرس را بر اساس شناسه پیدا می‌کند.
        /// </summary>
        /// <param name="id">شناسه آدرس.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>آدرس پیدا شده یا مقدار null.</returns>
        private async Task<Address> FindAddress(int id, CancellationToken cancellationToken)
        => await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

}
