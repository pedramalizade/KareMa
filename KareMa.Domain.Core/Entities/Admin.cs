using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
