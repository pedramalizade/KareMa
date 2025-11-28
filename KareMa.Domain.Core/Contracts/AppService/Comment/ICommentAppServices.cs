namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICommentAppServices
    {
        /// <summary>
        /// ایجاد نظر جدید.
        /// </summary>
        Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی نظر.
        /// </summary>
        Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// حذف نظر بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نظر بر اساس شناسه.
        /// </summary>
        Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه نظرات.
        /// </summary>
        Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// ثبت امتیاز برای کارشناس.
        /// </summary>
        Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken);

        /// <summary>
        /// پذیرش نظر.
        /// </summary>
        Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken);

        /// <summary>
        /// رد نظر.
        /// </summary>
        Task RejectCommentAsync(int commentId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت نظرات اخیر.
        /// </summary>
        Task<List<RecentCommentDto>> GetRecentCommentsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// تعداد کل نظرات.
        /// </summary>
        Task<int> CommentCountAsync(CancellationToken cancellationToken);
    }
}
