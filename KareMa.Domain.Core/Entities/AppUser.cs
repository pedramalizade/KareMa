using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public Admin? Admin { get; set; }
        public int? AdminId { get; set; }
        public Customer? Customer { get; set; }
        public int? CustomerId { get; set; }
        public Expert? Expert { get; set; }
        public int? ExpertId { get; set; }
    }
}
