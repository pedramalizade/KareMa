using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class City
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Address>? Address { get; set; }
    }
}
