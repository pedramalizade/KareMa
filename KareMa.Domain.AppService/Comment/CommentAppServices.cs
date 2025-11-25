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

        /// <summary>تایید کامنت.</summary>
        public async Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken)
    => await _commentServices.AcceptCommentAsync(commentId, cancellationToken);

        /// <summary>ایجاد کامنت.</summary>
        public async Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
            => await _commentServices.CreateAsync(commentCreateDto, cancellationToken);

        /// <summary>حذف کامنت.</summary>
        public async Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken)
            => await _commentServices.DeleteAsync(CommentId, cancellationToken);

        /// <summary>دریافت همه کامنت‌ها.</summary>
        public async Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken)
            => await _commentServices.GetAllAsync(cancellationToken);

        /// <summary>دریافت کامنت با شناسه.</summary>
        public async Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken)
            => await _commentServices.GetByIdAsync(commentId, cancellationToken);

        /// <summary>ثبت امتیاز برای متخصص.</summary>
        public async Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken)
            => await _commentServices.SetScoreAsync(expertId, score, cancellationToken);

        /// <summary>بروزرسانی کامنت.</summary>
        public async Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
            => await _commentServices.UpdateAsync(commentUpdateDto, cancellationToken);

        /// <summary>تعداد کل کامنت‌ها.</summary>
        public async Task<int> CommentCountAsync(CancellationToken cancellationToken)
            => await _commentServices.CommentCountAsync(cancellationToken);

        /// <summary>دریافت کامنت‌های اخیر.</summary>
        public async Task<List<RecentCommentDto>> GetRecentCommentsAsync(CancellationToken cancellationToken)
        {
            var resentCount = _commentConfiguration.RecentCount;
            return await _commentServices.GetRecentCommentsAsync(resentCount, cancellationToken);
        }

        /// <summary>رد کامنت.</summary>
        public async Task RejectCommentAsync(int commentId, CancellationToken cancellationToken)
            => await _commentServices.RejectCommentAsync(commentId, cancellationToken);
    }
}
