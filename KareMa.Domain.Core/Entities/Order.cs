using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace KareMa.Domain.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Expert? Expert { get; set; }
        public int? ExpertId { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        [DisplayName("عکس ها")]
        public string? Image { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public DateTime? DoneAt { get; set; }


        //public int Id { get; set; }
        //[MaxLength(50)]
        //public string Title { get; set; }
        //[MaxLength(1000)]
        //public string Description { get; set; }
        //public StatusEnum Status { get; set; }
        //public Customer Customer { get; set; }
        //public int CustomerId { get; set; }
        //public Expert Expert { get; set; }
        //public int ExpertId { get; set; }
        //public int ServiceId { get; set; }
        //[DisplayName("عکس")]
        //public string? Image { get; set; }
        //public bool IsDeleted { get; set; } = false;
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime RequestForTime { get; set; } = DateTime.Now;
        //public DateTime? DoneAt { get; set; }
        //public Service Service { get; set; }
        //public List<Suggestion>? Suggestions { get; set; }
    }
}
