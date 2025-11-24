namespace KareMa.Domain.Service
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        /// <summary>تأیید کامنت</summary>
        public async Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken)
            => await _commentRepository.AcceptCommentAsync(commentId, cancellationToken);

        /// <summary>تعداد کامنت‌ها</summary>
        public async Task<int> CommentCountAsync(CancellationToken cancellationToken)
            => await _commentRepository.CommentCountAsync(cancellationToken);

        /// <summary>ایجاد کامنت جدید</summary>
        public async Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken)
            => await _commentRepository.CreateAsync(commentCreateDto, cancellationToken);

        /// <summary>حذف کامنت</summary>
        public async Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken)
            => await _commentRepository.DeleteAsync(CommentId, cancellationToken);

        /// <summary>دریافت همه کامنت‌ها</summary>
        public async Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken)
            => await _commentRepository.GetAllAsync(cancellationToken);

        /// <summary>دریافت کامنت با شناسه</summary>
        public async Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken)
            => await _commentRepository.GetByIdAsync(commentId, cancellationToken);

        /// <summary>دریافت آخرین کامنت‌ها</summary>
        public async Task<List<RecentCommentDto>> GetRecentCommentsAsync(int count, CancellationToken cancellationToken)
            => await _commentRepository.GetRecentCommentsAsync(count, cancellationToken);

        /// <summary>رد کامنت</summary>
        public async Task RejectCommentAsync(int commentId, CancellationToken cancellationToken)
            => await _commentRepository.RejectCommentAsync(commentId, cancellationToken);

        /// <summary>ثبت امتیاز برای متخصص</summary>
        public async Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken)
            => await _commentRepository.SetScoreAsync(expertId, score, cancellationToken);

        /// <summary>ویرایش کامنت</summary>
        public async Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken)
            => await _commentRepository.UpdateAsync(commentUpdateDto, cancellationToken);
    }
}
