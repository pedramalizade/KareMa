namespace KareMa.Domain.Service
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken)
  => await _commentRepository.AcceptCommentAsync(commentId, cancellationToken);
        public async Task<int> CommentCountAsync(CancellationToken cancellationToken)
         => await _commentRepository.CommentCountAsync(cancellationToken);
        public async Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
     => await _commentRepository.CreateAsync(commentCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken)
  => await _commentRepository.DeleteAsync(CommentId, cancellationToken);
        public async Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken)
         => await _commentRepository.GetAllAsync(cancellationToken);
        public async Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken)
     => await _commentRepository.GetByIdAsync(commentId, cancellationToken);
        public async Task<List<RecentCommentDto>> GetRecentCommentsAsync(int count, CancellationToken cancellationToken)
          => await _commentRepository.GetRecentCommentsAsync(count, cancellationToken);
        public async Task RejectCommentAsync(int commentId, CancellationToken cancellationToken)
          => await _commentRepository.RejectCommentAsync(commentId, cancellationToken);
        public async Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken)
          => await _commentRepository.SetScoreAsync(expertId, score, cancellationToken);
        public async Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
          => await _commentRepository.UpdateAsync(commentUpdateDto, cancellationToken);
    }
}
