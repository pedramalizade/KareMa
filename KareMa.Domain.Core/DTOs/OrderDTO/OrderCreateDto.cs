using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.DTOs.OrderDTO
{
    public class OrderCreateDto
    {
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("وضعیت")]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        [DisplayName("عکس ها")]
        public string? Image { get; set; }
    }
}
