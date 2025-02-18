using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;

namespace KareMa.Domain.Core.DTOs.SuggestionDTO
{
    public class SuggestionCreateDto
    {
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        public Expert Expert { get; set; }
        public int ExpertId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        [DisplayName("قیمت پیشنهادی")]
        public int Price { get; set; }
    }
}
