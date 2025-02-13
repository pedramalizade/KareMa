using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.ImageDTO
{
    public class ImageUpdateDto
    {
        public int Id { get; set; }
        [DisplayName("توضیحات عکس")]
        public string? Alt { get; set; }
        [DisplayName("آدرس تصویر")]
        public string ImageAddress { get; set; }
        public Expert? Expert { get; set; }
        public int? ExpertId { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public Service? Service { get; set; }
        public int? ServiceId { get; set; }
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int? SubCategoryId { get; set; }
    }
}
