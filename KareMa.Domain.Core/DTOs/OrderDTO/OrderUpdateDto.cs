using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.OrderDTO
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("وضعیت")]
        public StatusEnum Status { get; set; }
        public Entities.Expert? Expert { get; set; }
        public int? ExpertId { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        [DisplayName("عکس ها")]
        public string? Image { get; set; }
        public DateTime? DoneAt { get; set; }
    }
}
