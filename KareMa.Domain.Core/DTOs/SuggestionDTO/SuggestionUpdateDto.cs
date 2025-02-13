using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.SuggestionDTO
{
    public class SuggestionUpdateDto
    {
        public int Id { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("قیمت پیشنهادی")]
        public int Price { get; set; }
    }
}
