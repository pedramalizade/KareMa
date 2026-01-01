namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(AppDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>ایجاد مشتری جدید.</summary>
        public async Task<bool> CreateAsync(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Customer()
            {
                FirstName = customerCreateDto.FirstName,
                LastName = customerCreateDto.LastName,
                Gender = customerCreateDto.Gender,
                PhoneNumber = customerCreateDto.PhoneNumber,
                BankCardNumber = customerCreateDto.BankCardNumber,
                Addresses = customerCreateDto.Addresses,
                Image = customerCreateDto.Image,
                AppUserId = customerCreateDto.AppUserId,
                Balance = customerCreateDto.Balance ?? 0
            };
            await _context.Customers.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>حذف نرم مشتری.</summary>
        public async Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("حذف مشتری آغاز شد.", customerId);

            var targetModel = await FindCustomer(customerId, cancellationToken);
            if (targetModel == null)
            {
                _logger.LogWarning("مشتری یافت نشد.", customerId);
                return false;
            }

            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("مشتری با موفقیت حذف شد (به‌صورت نرم).", customerId);
            return true;
        }

        /// <summary>دریافت مشتریان فعال.</summary>
        public async Task<List<GetCustomerDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await Queryable
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .Select(c => new GetCustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Image = c.Image,
                    Balance = c.Balance
                }).ToListAsync(cancellationToken);

            _logger.LogInformation(" مشتری فعال یافت شد.", customers.Count);
            return customers;
        }

        /// <summary>دریافت مشتری با شناسه.</summary>
        public async Task<Customer> GetByIdAsync(int customerId, CancellationToken cancellationToken)
        {
            return await FindCustomer(customerId, cancellationToken);
        }

        /// <summary>به‌روزرسانی اطلاعات مشتری.</summary>
        public async Task<bool> UpdateAsync(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("به‌روزرسانی مشتری آغاز شد.", customerUpdateDto.Id);

            var targetModel = await Queryable
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == customerUpdateDto.Id && !c.IsDeleted, cancellationToken);

            if (targetModel == null)
            {
                _logger.LogWarning("مشتری یافت نشد.", customerUpdateDto.Id);
                return false;
            }

            targetModel.FirstName = customerUpdateDto.FirstName;
            targetModel.LastName = customerUpdateDto.LastName;
            targetModel.Balance = customerUpdateDto.Balance;
            targetModel.Image = customerUpdateDto.Image;
            targetModel.BankCardNumber = customerUpdateDto.BankCardNumber;
            targetModel.PhoneNumber = customerUpdateDto.PhoneNumber;
            targetModel.Gender = customerUpdateDto.Gender;

            if (customerUpdateDto.Address != null)
            {
                if (targetModel.Addresses != null)
                {
                    targetModel.Addresses.Title = customerUpdateDto.Address.Title;
                    targetModel.Addresses.CityId = customerUpdateDto.Address.CityId;
                    targetModel.Addresses.Street = customerUpdateDto.Address.Street;
                    targetModel.Addresses.Area = customerUpdateDto.Address.Area;
                    targetModel.Addresses.PostalCode = customerUpdateDto.Address.PostalCode;

                    _logger.LogInformation("آدرس موجود برای مشتری به‌روزرسانی شد.", customerUpdateDto.Id);
                }
                else
                {
                    targetModel.Addresses = customerUpdateDto.Address;
                    _logger.LogInformation("آدرس جدید برای مشتری اضافه شد.", customerUpdateDto.Id);
                }
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("مشتری با موفقیت به‌روزرسانی شد.", customerUpdateDto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا هنگام ذخیره تغییرات مشتری با شناسه ", customerUpdateDto.Id);
                throw;
            }
        }

        /// <summary>دریافت خلاصه اطلاعات مشتری.</summary>
        public async Task<CustomerSummaryDto> GetCustomerSummaryAsync(int id, CancellationToken cancellationToken)
        {
            var target = await Queryable.Where(a => a.Id == id && a.IsDeleted == false)
                .Select(c => new CustomerSummaryDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BankCardNumber = c.BankCardNumber,
                    PhoneNumber = c.PhoneNumber,
                    Balance = c.Balance,
                    Gender = c.Gender,
                    Addresses = c.Addresses,
                    Comments = c.Comments,
                    Orders = c.Orders
                }).FirstOrDefaultAsync(cancellationToken);
            if (target is not null)
            {
                return target;
            }
            return new CustomerSummaryDto();
        }

        /// <summary>اطلاعات لازم فرم ویرایش مشتری.</summary>
        public async Task<CustomerUpdateDto> GetCustomerUpdateInfoAsync(int customerId, CancellationToken cancellationToken)
        {
            var targetCustomer = await Queryable
                .AsNoTracking()
                .Include(c => c.Addresses)
                .Where(c => c.Id == customerId && !c.IsDeleted)
                .Select(c => new CustomerUpdateDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Addresses,
                    Balance = c.Balance,
                    BankCardNumber = c.BankCardNumber,
                    Gender = c.Gender,
                    Image = c.Image
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (targetCustomer == null)
            {
                return null;
            }

            return targetCustomer;
        }

        /// <summary>یافتن شناسه مشتری با AppUserId.</summary>
        public async Task<int> FindCustomerIdWithApplicationUser(int appUserId, CancellationToken cancellationToken)
        {
            var targetCustomer = await Queryable.FirstOrDefaultAsync(c => c.AppUserId == appUserId, cancellationToken);
            var customerId = targetCustomer.Id;
            return customerId;
        }

        /// <summary>تعداد کل مشتریان.</summary>
        public async Task<int> CustomerCountAsync(CancellationToken cancellationToken)
  => await Queryable.CountAsync(cancellationToken);


        /// <summary>جستجوی مشتری با شناسه.</summary>
        private async Task<Customer> FindCustomer(int id, CancellationToken cancellationToken)
     => await Queryable.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        /// <summary>دریافت اطلاعات مشتری برای ویرایش.</summary>
        public async Task<CustomerUpdateDto?> CustomerUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            return await Queryable.Select(a => new CustomerUpdateDto
            {
                Id = id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Address = a.Addresses,
                Image = a.Image,
                Gender = a.Gender,
                Balance = a.Balance,
                PhoneNumber = a.PhoneNumber

            }).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        /// <summary>آپدیت موجودی مشتری.</summary>
        public async Task UpdateBalanceAsync(int customerId, decimal newBalance, CancellationToken cancellationToken)
        {
            _logger.LogInformation("به‌روزرسانی موجودی مشتری به مقدار آغاز شد.", customerId, newBalance);

            var customer = await Queryable
                .FirstOrDefaultAsync(c => c.Id == customerId && !c.IsDeleted, cancellationToken);

            if (customer == null)
            {
                _logger.LogWarning("مشتری یافت نشد یا حذف شده است.", customerId);
                throw new Exception($"مشتری با شناسه {customerId} یافت نشد.");
            }

            customer.Balance = newBalance;
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("موجودی مشتری با موفقیت به‌روزرسانی شد.", customerId);
        }

        /// <summary>دریافت مشتری غیرحذف‌شده.</summary>
        public async Task<Customer> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("در حال دریافت اطلاعات مشتری با شناسه ", customerId);

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == customerId && !c.IsDeleted, cancellationToken);

            if (customer == null)
            {
                _logger.LogWarning("مشتری یافت نشد یا حذف شده است.", customerId);
            }

            return customer;
        }
    }
}
