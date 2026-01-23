namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>ایجاد سفارش.</summary>
        public async Task<bool> CreateAsync(OrderCreateDto orderCreateDto, CancellationToken cancellationToken)
        {
            var newModel = new Order()
            {
                Title = orderCreateDto.Title,
                Description = orderCreateDto.Description,
                Status = StatusEnum.AwaitingSuggestionExperts,
                CustomerId = orderCreateDto.CustomerId,
                ServiceId = orderCreateDto.ServiceId,
                Image = orderCreateDto.Image,
                RequesteForTime = orderCreateDto.Date
            };
            await _context.Orders.AddAsync(newModel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <summary>حذف منطقی سفارش.</summary>
        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
        {
            var targetModel = await FindOrder(orderId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <summary>دریافت همه سفارش‌ها.</summary>
        public async Task<List<GetOrderDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await Queryable
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Suggestions)
                .ThenInclude(s => s.Expert)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    Status = o.Status,
                    Customer = o.Customer,
                    Service = o.Service,
                    Image = o.Image,
                    Suggestions = o.Suggestions
                })
                .ToListAsync(cancellationToken);

            return orders;
        }

        /// <summary>دریافت سفارش با شناسه.</summary>
        public async Task<Order> GetByIdAsync(int orderId, CancellationToken cancellationToken)
            => await FindOrder(orderId, cancellationToken);

        /// <summary>به‌روزرسانی سفارش.</summary>
        public async Task<bool> UpdateAsync(OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindOrder(orderUpdateDto.Id, cancellationToken);

            targetModel.Title = orderUpdateDto.Title;
            targetModel.Description = orderUpdateDto.Description;
            targetModel.Status = orderUpdateDto.Status;
            targetModel.Service = orderUpdateDto.Service;
            targetModel.ServiceId = orderUpdateDto.ServiceId;
            targetModel.Image = orderUpdateDto.Image;
            targetModel.DoneAt = orderUpdateDto.DoneAt;
            targetModel.Suggestions = orderUpdateDto.Suggestions;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        /// <summary>تغییر وضعیت سفارش.</summary>
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            var targetModel = await FindOrder(orderId, cancellationToken);

            if (targetModel == null)
                return false;

            var allowedStatuses = GetAllowedStatuses(targetModel.Status);

            if (!allowedStatuses.Contains(status))
                return false;

            targetModel.Status = status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <summary>وضعیت‌های مجاز بعدی.</summary>
        private List<StatusEnum> GetAllowedStatuses(StatusEnum currentStatus)
        {
            switch (currentStatus)
            {
                case StatusEnum.AwaitingSuggestionExperts:
                    return new List<StatusEnum> { StatusEnum.AwaitingSuggestionExperts, StatusEnum.AwaitingCustomerConfirmation };

                case StatusEnum.AwaitingCustomerConfirmation:
                    return new List<StatusEnum> { StatusEnum.AwaitingCustomerConfirmation, StatusEnum.Confirmed, StatusEnum.NotConfirmed };

                case StatusEnum.Confirmed:
                    return new List<StatusEnum> { StatusEnum.Confirmed, StatusEnum.Done };

                case StatusEnum.Done:
                    return new List<StatusEnum> { StatusEnum.Done };

                default:
                    return new List<StatusEnum> { currentStatus };
            }
        }
        /// <summary>تعداد سفارش‌ها.</summary>
        public async Task<int> OrderCountAsync(CancellationToken cancellationToken)
          => await Queryable.CountAsync(cancellationToken);
        /// <summary>سفارش‌های مشتری.</summary>
        public async Task<List<GetOrderDto>> GetOrdersAsync(int customerId, CancellationToken cancellationToken)
        {
            var target = await Queryable.Where(o => o.Customer.Id == customerId && o.IsDeleted == false)
                .Select(o => new GetOrderDto
                {
                    Customer = o.Customer,
                    Description = o.Description,
                    Image = o.Image,
                    Id = o.Id,
                    Service = o.Service,
                    Status = o.Status,
                    Title = o.Title,
                    Suggestions = o.Suggestions.Select(x => new Suggestion()
                    {
                        ExpertId = x.ExpertId,
                        Expert = x.Expert,
                        Id = x.Id,
                        Description = x.Description,
                        Price = x.Price,
                        SuggestedDate = x.SuggestedDate,
                        Status = x.Status,
                    }).ToList()

                }).ToListAsync(cancellationToken);

            return target;
        }
        /// <summary>تأیید سفارش.</summary>
        public async Task AcceptOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var target = await Queryable.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
            target.Status = StatusEnum.Confirmed;

            await _context.SaveChangesAsync(cancellationToken);
        }
        /// <summary>انجام‌شدن سفارش.</summary>
        public async Task DoneOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var targetOrder = await Queryable.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
            targetOrder.Status = StatusEnum.Done;
            targetOrder.DoneAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
        /// <summary>سفارش‌ها بر اساس سرویس.</summary>
        public async Task<List<OrdersByServiceIdsDto>> GetOrdersByServiceIdsAsync(List<int> serviceIds, CancellationToken cancellationToken)
        {
            return await Queryable.Where(o => serviceIds.Contains(o.ServiceId))
                  .Select(o => new OrdersByServiceIdsDto
                  {
                      Id = o.Id,
                      Title = o.Title,
                      Description = o.Description,
                      Image = o.Image,
                      CustomerId = o.CustomerId,
                      Customer = o.Customer,
                      Service = o.Service,
                      ServiceId = o.ServiceId,
                      Status = o.Status,
                      RequesteForTime = o.RequesteForTime

                  }).ToListAsync(cancellationToken);
        }

        /// <summary>آیا سفارش انجام شده؟</summary>
        public async Task<bool> OrderIsDoneAsync(int orderId, CancellationToken cancellationToken)
        {
            var targetOrder = await Queryable.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (targetOrder.Status == StatusEnum.Done) return true;

            return false;
        }
        /// <summary>جستجوی سفارش.</summary>
        private async Task<Order> FindOrder(int orderId, CancellationToken cancellationToken)
          => await Queryable.FirstOrDefaultAsync(a => a.Id == orderId, cancellationToken);
    }
}
