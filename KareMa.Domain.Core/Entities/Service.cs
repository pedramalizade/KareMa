using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KareMa.Domain.Core.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [DisplayName("نام سرویس")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public List<Expert>? Experts { get; set; }
        public List<Order>? Orders { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
