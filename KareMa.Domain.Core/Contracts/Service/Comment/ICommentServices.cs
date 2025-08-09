namespace KareMa.Domain.Core.Contracts.Service
{
    public interface ICommentServices
    {
        Task<bool> CreateAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int CommentId, CancellationToken cancellationToken);
        Task<Comment> GetByIdAsync(int commentId, CancellationToken cancellationToken);
        Task<List<GetCommentsDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> SetScoreAsync(int expertId, int score, CancellationToken cancellationToken);
        Task AcceptCommentAsync(int commentId, CancellationToken cancellationToken);
        Task<List<RecentCommentDto>> GetRecentCommentsAsync(int count, CancellationToken cancellationToken);
        Task<int> CommentCountAsync(CancellationToken cancellationToken);
        Task RejectCommentAsync(int commentId, CancellationToken cancellationToken);
    }
}
