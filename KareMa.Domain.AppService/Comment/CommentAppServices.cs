namespace KareMa.Domain.AppService
{
    public class CommentAppServices : ICommentAppServices
    {
        private readonly ICommentServices _commentServices;
        private readonly CommentConfiguration _commentConfiguration;
        public CommentAppServices(ICommentServices commentServices, CommentConfiguration commentConfiguration)
        {
            _commentServices = commentServices;
            _commentConfiguration = commentConfiguration;
        }
        public async Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken)
           => await _commentServices.AcceptCommentAsync(commentId, cancellationToken);
        public async Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
  => await _commentServices.CreateAsync(commentCreateDto, cancellationToken);
        public async Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken)
        => await _commentServices.DeleteAsync(CommentId, cancellationToken);
        public async Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken)
       => await _commentServices.GetAllAsync(cancellationToken);
        public async Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken)
          => await _commentServices.GetByIdAsync(commentId, cancellationToken);
        public async Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken)
      => await _commentServices.SetScoreAsync(expertId, score, cancellationToken);
        public async Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
          => await _commentServices.UpdateAsync(commentUpdateDto, cancellationToken);
        public async Task<int> CommentCountAsync(CancellationToken cancellationToken)
          => await _commentServices.CommentCountAsync(cancellationToken);
        public async Task<List<RecentCommentDto>> GetRecentCommentsAsync(CancellationToken cancellationToken)
        {
            var resentCount = _commentConfiguration.RecentCount;
            return await _commentServices.GetRecentCommentsAsync(resentCount, cancellationToken);
        }
        public async Task RejectCommentAsync(int commentId, CancellationToken cancellationToken)
         => await _commentServices.RejectCommentAsync(commentId, cancellationToken);
    }
}
