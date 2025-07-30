namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ICommentAppServices
    {
        Task<bool> Create(CommentCreateDto commentCreateDto, CancellationToken cancellationToken);
        Task<bool> Update(CommentUpdateDto commentUpdateDto, CancellationToken cancellationToken);
        Task<bool> Delete(int CommentId, CancellationToken cancellationToken);
        Task<Comment> GetById(int commentId, CancellationToken cancellationToken);
        Task<List<GetCommentsDto>> GetAll(CancellationToken cancellationToken);
        Task<bool> SetScore(int expertId, int score, CancellationToken cancellationToken);
        Task AcceptComment(int commentId, CancellationToken cancellationToken);
        Task RejectComment(int commentId, CancellationToken cancellationToken);
        Task<List<RecentCommentDto>> GetRecentComments(CancellationToken cancellationToken);
        Task<int> CommentCount(CancellationToken cancellationToken);
    }
}
