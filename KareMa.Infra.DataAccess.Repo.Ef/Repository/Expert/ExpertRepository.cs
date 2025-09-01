namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpertRepository> _logger;
        public ExpertRepository(AppDbContext context, ILogger<ExpertRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var existingExpert = await _context.Experts
                    .AsNoTracking()
                    .AnyAsync(e => e.AppUserId == expertCreateDto.AppUserId && !e.IsDeleted, cancellationToken);

                if (existingExpert)
                {
                    _logger.LogWarning("متخصص از قبل وجود دارد.", expertCreateDto.AppUserId);
                    throw new InvalidOperationException($"متخصصی با {expertCreateDto.AppUserId} قبلاً ثبت شده است.");
                }

                var newExpert = new Expert
                {
                    AppUserId = expertCreateDto.AppUserId,
                    FirstName = expertCreateDto.FirstName,
                    LastName = expertCreateDto.LastName,
                    Gender = expertCreateDto.Gender,
                    PhoneNumber = expertCreateDto.PhoneNumber,
                    Address = expertCreateDto.Address,
                    BankCardNumber = expertCreateDto.BankCardNumber,
                    Balance = expertCreateDto.Balance,
                    BirthDate = expertCreateDto.BirthDate,
                    Image = expertCreateDto.Image,
                    Services = expertCreateDto.Services != null
                        ? _context.Services.Where(s => expertCreateDto.Services.Contains(s.Id)).ToList()
                        : new List<Service>()
                };

                _logger.LogInformation("در حال ایجاد متخصص",
                    newExpert.AppUserId, newExpert.Services?.Count ?? 0);

                await _context.Experts.AddAsync(newExpert, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("متخصص با موفقیت ذخیره شد.");
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int expertId, CancellationToken cancellationToken)
        {

            var targetExpert = await FindExpert(expertId, cancellationToken);
            if (targetExpert == null)
            {
                return false;
            }

            targetExpert.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)
        {
            var experts = await _context.Experts
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync(cancellationToken);

            return experts;
        }

        public async Task<Expert> GetByIdAsync(int expertId, CancellationToken cancellationToken)
            => await FindExpert(expertId, cancellationToken);

        public async Task<bool> UpdateAsync(ExpertUpdateDto expertUpdateDto, CancellationToken cancellationToken)
        {
            var targetExpert = await _context.Experts
                .Include(e => e.Services)
                .FirstOrDefaultAsync(e => e.Id == expertUpdateDto.Id && !e.IsDeleted, cancellationToken);

            if (targetExpert == null)
            {
                _logger.LogWarning("متخصص با شناسه {ExpertId} یافت نشد.", expertUpdateDto.Id);
                return false;
            }

            targetExpert.FirstName = expertUpdateDto.FirstName;
            targetExpert.LastName = expertUpdateDto.LastName;
            targetExpert.PhoneNumber = expertUpdateDto.PhoneNumber;
            targetExpert.Gender = expertUpdateDto.Gender;
            targetExpert.BankCardNumber = expertUpdateDto.BankCardNumber;
            targetExpert.Balance = expertUpdateDto.Balance;
            targetExpert.BirthDate = expertUpdateDto.BirthDate;
            targetExpert.Bio = expertUpdateDto.Bio;

            if (expertUpdateDto.Image != null)
                targetExpert.Image = expertUpdateDto.Image;

            _logger.LogInformation("سرویس‌های انتخاب شده برای ذخیره: {ServiceIds}", string.Join(", ", expertUpdateDto.ServiceIds ?? new List<int>()));

            targetExpert.Services ??= new List<Service>();
            targetExpert.Services.Clear();

            if (expertUpdateDto.ServiceIds != null && expertUpdateDto.ServiceIds.Any())
            {
                var services = await _context.Services
                    .Where(s => expertUpdateDto.ServiceIds.Contains(s.Id))
                    .ToListAsync(cancellationToken);

                if (services.Count != expertUpdateDto.ServiceIds.Count)
                {
                    var missing = expertUpdateDto.ServiceIds.Except(services.Select(s => s.Id));
                    _logger.LogWarning("برخی شناسه‌های سرویس یافت نشدند", string.Join(", ", missing));
                    return false;
                }

                targetExpert.Services.AddRange(services);
                _logger.LogInformation("سرویس‌ها با موفقیت به متخصص اختصاص داده شدند", string.Join(", ", targetExpert.Services.Select(s => s.Id)));
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("متخصص با موفقیت به‌روزرسانی شد.", expertUpdateDto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا هنگام ذخیره تغییرات متخصص با شناسه", expertUpdateDto.Id);
                throw new Exception("خطا در ذخیره تغییرات در دیتابیس", ex);
            }
        }

        public async Task<int> ExpertCountAsync(CancellationToken cancellationToken)
            => await _context.Experts.CountAsync(cancellationToken);

        public async Task<ExpertSummaryDto> GetExpertSummaryAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("در حال دریافت خلاصه اطلاعات متخصص با شناسه", id);

            var expert = await _context.Experts
                .Include(e => e.Services)
                .Include(e => e.Comments)
                .Where(e => e.Id == id && !e.IsDeleted)
                .Select(e => new ExpertSummaryDto
                {
                    Id = e.Id,
                    Comments = e.Comments != null
                        ? e.Comments.Where(c => c.IsAccept && !c.IsDeleted)
                            .Select(c => new Comment
                            {
                                Customer = c.Customer,
                                Score = c.Score,
                                Title = c.Title,
                                Description = c.Description,
                                CreatedAt = c.CreatedAt,
                                IsAccept = c.IsAccept,
                                IsDeleted = c.IsDeleted
                            }).ToList()
                        : new List<Comment>(),
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    ProfileImage = e.Image,
                    Services = e.Services ?? new List<Service>(),
                    Balance = e.Balance
                }).FirstOrDefaultAsync(cancellationToken);

            if (expert == null)
            {
                _logger.LogWarning("متخصص یافت نشد یا حذف شده است.", id);
                return new ExpertSummaryDto { Id = id, Comments = new List<Comment>(), Services = new List<Service>(), Balance = 0 };
            }

            _logger.LogInformation("خلاصه متخصص: موجودی={Balance} | تعداد دیدگاه={CommentCount} | تعداد سرویس={ServiceCount}",
                expert.Balance, expert.Comments.Count, expert.Services.Count);

            return expert;
        }

        public async Task<int> ExpertCommentCountAsync(int id, CancellationToken cancellationToken)
            => await _context.Experts.Where(e => e.Id == id).SelectMany(e => e.Comments).CountAsync();

        public async Task<int> ExpertAverageScoresAsync(int id, CancellationToken cancellationToken)
        {
            var expert = await _context.Experts.Include(e => e.Comments).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (expert == null || expert.Comments == null || !expert.Comments.Any())
                return 0;

            return (int)expert.Comments.Select(c => c.Score).Average();
        }

        public async Task<int> ExpertOrderCountAsync(int id, CancellationToken cancellationToken)
        {
            var suggestions = await _context.Experts.Where(e => e.Id == id)
                .SelectMany(e => e.Suggestions)
                .ToListAsync(cancellationToken);

            return suggestions.Count(o => o.Status == StatusEnum.Done);
        }

        public async Task<List<int>> GetExpertServiceIdsAsync(int id, CancellationToken cancellationToken)
            => (await _context.Experts.Where(e => e.Id == id).SelectMany(e => e.Services).ToListAsync(cancellationToken))
                .Select(s => s.Id).ToList();

        public async Task<ExpertUpdateDto> ExpertUpdateInfoAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Experts.Include(e => e.Services)
                .Select(e => new ExpertUpdateDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    Gender = e.Gender,
                    Balance = e.Balance,
                    BirthDate = e.BirthDate,
                    BankCardNumber = e.BankCardNumber,
                    Image = e.Image,
                    Bio = e.Bio,
                    ServiceIds = e.Services.Select(s => s.Id).ToList()
                }).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            _logger.LogInformation("اطلاعات به‌روزرسانی متخصص {ExpertId}: سرویس‌ها = {ServiceIds}", id, string.Join(", ", result?.ServiceIds ?? new List<int>()));
            return result;
        }

        public async Task<ExpertNameDto> GetExpertNameAsync(int id, CancellationToken cancellationToken)
            => await _context.Experts.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new ExpertNameDto { FirstName = e.FirstName, LastName = e.LastName, Balance = e.Balance })
                .FirstOrDefaultAsync(cancellationToken) ?? new ExpertNameDto();

        public async Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken)
        {

            var expert = await _context.Experts
                .FirstOrDefaultAsync(e => e.Id == expertId && !e.IsDeleted, cancellationToken);

            if (expert == null)
                _logger.LogWarning($"متخصص با شناسه {expertId} یافت نشد یا حذف شده است.", expertId);

            return expert;
        }

        public async Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"در حال به‌روزرسانی موجودی متخصص با شناسه {expertId} به {newBalance}", expertId, newBalance);

            var expert = await _context.Experts
                .FirstOrDefaultAsync(e => e.Id == expertId && !e.IsDeleted, cancellationToken);

            if (expert == null)
            {
                _logger.LogWarning($"متخصص با شناسه {expertId} یافت نشد یا حذف شده است.", expertId);
                throw new Exception($"متخصص با شناسه {expertId} یافت نشد یا حذف شده است.");
            }

            expert.Balance = newBalance;
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"موجودی متخصص با شناسه {expertId} با موفقیت به‌روزرسانی شد.", expertId);
        }

        private async Task<Expert> FindExpert(int id, CancellationToken cancellationToken)
            => await _context.Experts.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    }
}

