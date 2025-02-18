using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.CommentDTO
{
    public class RecentCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Expert Expert { get; set; }
        public int Score { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
