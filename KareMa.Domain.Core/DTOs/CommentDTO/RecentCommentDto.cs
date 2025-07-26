namespace KareMa.Domain.Core.DTOs.CommentDTO
{
    public class RecentCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Entities.Expert Expert { get; set; }
        public int Score { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
