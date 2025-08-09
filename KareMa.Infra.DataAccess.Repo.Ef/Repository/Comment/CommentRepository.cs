namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
        {

            if (commentCreateDto == null || commentCreateDto.CustomerId <= 0 || commentCreateDto.ExpertId <= 0)
                return false;

            var customerExists = await _context.Customers.AnyAsync(c => c.Id == commentCreateDto.CustomerId && !c.IsDeleted, cancellationToken);
            var expertExists = await _context.Experts.AnyAsync(e => e.Id == commentCreateDto.ExpertId && !e.IsDeleted, cancellationToken);
            if (!customerExists || !expertExists)
                return false;

            var orderCompleted = await _context.Orders
        .AnyAsync(o => o.CustomerId == commentCreateDto.CustomerId &&
                       !o.IsDeleted &&
                       (o.ExpertId == commentCreateDto.ExpertId || o.ExpertId == null) &&
                       o.Status == StatusEnum.Done, cancellationToken);

            if (!orderCompleted)
            {
                return false;
            }

            var newModel = new Comment()
            {
                Title = commentCreateDto.Title,
                Description = commentCreateDto.Description,
                Score = commentCreateDto.Score,
                CustomerId = commentCreateDto.CustomerId,
                ExpertId = commentCreateDto.ExpertId,
                CreatedAt = DateTime.UtcNow,
                IsAccept = true,
                IsDeleted = false
            };

            try
            {
                await _context.Comments.AddAsync(newModel, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.AsNoTracking()
                 .Select(c => new GetCommentsDto
                 {
                     Id = c.Id,
                     Title = c.Title,
                     CustomerName = c.Customer.FirstName,
                     CustomerFamily = c.Customer.LastName,
                     CustomerId = c.CustomerId,
                     ExpertName = c.Expert.FirstName,
                     ExpertFamily = c.Expert.LastName,
                     ExpertId = c.ExpertId,
                     Description = c.Description,
                     IsActive = c.IsAccept,
                     IsDeleted = c.IsDeleted,
                 }).ToListAsync(cancellationToken);
            return comments;
        }


        public async Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken)
       => await FindComment(commentId, cancellationToken);

        public async Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentUpdateDto.Id, cancellationToken);

            targetModel.Title = commentUpdateDto.Title;
            targetModel.Description = commentUpdateDto.Description;
            targetModel.Score = commentUpdateDto.Score;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Comments.FirstOrDefaultAsync(c => c.ExpertId == expertId, cancellationToken);
            targetModel.Score = score;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task RejectCommentAsync(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<RecentCommentDto>> GetRecentCommentsAsync(int count, CancellationToken cancellationToken)
        {
            var recentComments = await _context.Comments.
                Select(c => new RecentCommentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Score = c.Score,
                    Expert = c.Expert,
                    CreateAt = c.CreatedAt,
                })
                .OrderByDescending(c => c.CreateAt)
                .Take(count)
                .ToListAsync(cancellationToken);
            return recentComments;
        }
        public async Task<int> CommentCountAsync(CancellationToken cancellationToken)
          => await _context.Comments.CountAsync(cancellationToken);

        private async Task<Comment> FindComment(int id, CancellationToken cancellationToken)
     => await _context.Comments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
