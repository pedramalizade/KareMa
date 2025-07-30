namespace KareMa.Infra.DataAccess.Repo.Ef.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CommentAppServices.Create called for ExpertId: {commentCreateDto?.ExpertId ?? -1}, CustomerId: {commentCreateDto?.CustomerId ?? -1}");

            if (commentCreateDto == null)
            {
                Console.WriteLine("Invalid input: CommentCreateDto is null.");
                return false;
            }
            if (commentCreateDto.CustomerId <= 0)
            {
                Console.WriteLine($"Invalid input: CustomerId is invalid ({commentCreateDto.CustomerId}).");
                return false;
            }
            if (commentCreateDto.ExpertId <= 0)
            {
                Console.WriteLine($"Invalid input: ExpertId is invalid ({commentCreateDto.ExpertId}).");
                return false;
            }

            var customerExists = await _context.Customers.AnyAsync(c => c.Id == commentCreateDto.CustomerId && !c.IsDeleted, cancellationToken);
            var expertExists = await _context.Experts.AnyAsync(e => e.Id == commentCreateDto.ExpertId && !e.IsDeleted, cancellationToken);
            if (!customerExists)
            {
                Console.WriteLine($"Customer with ID {commentCreateDto.CustomerId} not found or is deleted.");
                return false;
            }
            if (!expertExists)
            {
                Console.WriteLine($"Expert with ID {commentCreateDto.ExpertId} not found or is deleted.");
                return false;
            }

            var orders = await _context.Orders
                .Where(o => o.CustomerId == commentCreateDto.CustomerId && !o.IsDeleted)
                .Select(o => new { o.Id, o.ExpertId, o.Status })
                .ToListAsync(cancellationToken);

            var orderCompleted = orders.Any(o =>
                (o.ExpertId == commentCreateDto.ExpertId || o.ExpertId == null) &&
                o.Status == StatusEnum.Done
            );

            if (!orderCompleted)
            {
                Console.WriteLine($"No completed order found for CustomerId: {commentCreateDto.CustomerId} and ExpertId: {commentCreateDto.ExpertId}.");
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
                Console.WriteLine("Comment created successfully in database.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken)
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


        public async Task<Comment> GetById(int commentId, CancellationToken cancellationToken)
       => await FindComment(commentId, cancellationToken);

        public async Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentUpdateDto.Id, cancellationToken);

            targetModel.Title = commentUpdateDto.Title;
            targetModel.Description = commentUpdateDto.Description;
            targetModel.Score = commentUpdateDto.Score;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> SetScore(int expertId, int score, CancellationToken cancellationToken)
        {
            var targetModel = await _context.Comments.FirstOrDefaultAsync(c => c.ExpertId == expertId, cancellationToken);
            targetModel.Score = score;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task AcceptComment(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task RejectComment(int commentId, CancellationToken cancellationToken)
        {
            var targetModel = await FindComment(commentId, cancellationToken);
            targetModel.IsAccept = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<RecentCommentDto>> GetRecentComments(int count, CancellationToken cancellationToken)
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
        public async Task<int> CommentCount(CancellationToken cancellationToken)
          => await _context.Comments.CountAsync(cancellationToken);

        private async Task<Comment> FindComment(int id, CancellationToken cancellationToken)
     => await _context.Comments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
