namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class SuggestionRepository : ISuggestionRepository
    {
        private readonly AppDbContext _context;
        public SuggestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == suggestionCreateDto.OrderId && o.Status == StatusEnum.AwaitingSuggestionExperts, cancellationToken);
            if (order == null)
            {
                throw new Exception("سفارش باز نیست یا پیدا نشد.");
            }

            var existingSuggestion = await _context.Suggestions
                .AnyAsync(s => s.OrderId == suggestionCreateDto.OrderId && s.ExpertId == suggestionCreateDto.ExpertId, cancellationToken);
            if (existingSuggestion)
            {
                throw new Exception("شما قبلاً برای این سفارش پیشنهاد داده‌اید.");
            }

            var newModel = new Suggestion
            {
                Description = suggestionCreateDto.Description,
                ExpertId = suggestionCreateDto.ExpertId,
                OrderId = suggestionCreateDto.OrderId,
                Price = suggestionCreateDto.Price,
                SuggestedDate = suggestionCreateDto.SuggastionDate,
                CreateAt = DateTime.Now,
                IsDeleted = false,
                Status = StatusEnum.AwaitingCustomerConfirmation
            };

            await _context.Suggestions.AddAsync(newModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken)
        {
            var targetModel = await FindSuggestion(suggestionId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Suggestions.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
        {
            return await FindSuggestion(suggestionId, cancellationToken);
        }

        public async Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindSuggestion(suggestionUpdateDto.Id, cancellationToken);

            targetModel.Description = suggestionUpdateDto.Description;
            targetModel.Price = suggestionUpdateDto.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken)
        {
            var targetSuggestion = await _context.Suggestions
                .FirstOrDefaultAsync(s => s.Id == suggestionId && s.OrderId == orderId, cancellationToken);

            if (targetSuggestion == null || targetSuggestion.Status != StatusEnum.AwaitingCustomerConfirmation)
            {
                Console.WriteLine($"Suggestion ID: {suggestionId} not found or not awaiting confirmation for Order ID: {orderId}");
                return false;
            }

            var otherSuggestions = await _context.Suggestions
                .Where(s => s.OrderId == orderId && s.Id != suggestionId)
                .ToListAsync(cancellationToken);

            targetSuggestion.Status = StatusEnum.Confirmed;

            foreach (var suggestion in otherSuggestions)
            {
                if (suggestion.Status == StatusEnum.AwaitingCustomerConfirmation)
                {
                    suggestion.Status = StatusEnum.NotConfirmed;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"Suggestion ID: {suggestionId} confirmed for Order ID: {orderId}");
            return true;
        }

        public async Task<int> ConfrimedStatusCountAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Suggestions.Where(s => s.OrderId == orderId && s.Status == StatusEnum.Confirmed).CountAsync(cancellationToken);
        }

        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Suggestions.Where(s => s.ExpertId == id)
                .Select(s => new SuggestionsByExpertIdDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    ExpertId = s.ExpertId,
                    Price = s.Price,
                    Status = s.Status,
                    SuggestedDate = s.SuggestedDate,
                    OrderId = s.OrderId,
                    Order = new Order()
                    {
                        Service = s.Order.Service,
                        Title = s.Order.Title,
                        Description = s.Order.Description,
                        Image = s.Order.Image
                    }
                })
               .ToListAsync(cancellationToken);
        }

        public async Task DoneSuggestionAsync(int id, CancellationToken cancellationToken)
        {
            var targetSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            targetSuggestion.Status = StatusEnum.Done;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Suggestions.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);

            if (targetModel == null)
            {
                Console.WriteLine($"No suggestion found for OrderId: {orderId}");
                return false;
            }

            targetModel.Status = status;
            await _context.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"Suggestion status changed to {status} for OrderId: {orderId}");
            return true;
        }

        private async Task<Suggestion> FindSuggestion(int id, CancellationToken cancellationToken)
       => await _context.Suggestions.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
        {
            var suggestion = await _context.Suggestions
        .Include(s => s.Expert)
        .FirstOrDefaultAsync(s => s.Id == suggestionId, cancellationToken);

            if (suggestion == null) return null;

            return new SuggestionDto
            {
                Id = suggestion.Id,
                OrderId = suggestion.OrderId,
                ExpertId = suggestion.ExpertId,
                Expert = suggestion.Expert,
                Price = suggestion.Price, 
                Description = suggestion.Description,
                SuggestedDate = suggestion.SuggestedDate,
                Status = suggestion.Status
            };
        }
    }
}
