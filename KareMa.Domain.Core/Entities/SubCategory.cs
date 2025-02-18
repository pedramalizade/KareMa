using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public List<Service>? Services { get; set; }
        public string? Image { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        //public int Id { get; set; }
        //[MaxLength(100)]
        //public string Name { get; set; }
        //public Category? Category { get; set; }
        //public int CategoryId { get; set; }
        //public List<Service>? Services { get; set; }
        //public string? Image { get; set; }
        //public bool IsDeleted { get; set; } = false;
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
