using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum? Gender { get; set; }
        [MaxLength(11)]
        public string? BackUpPhoneNumber { get; set; }
        [MaxLength(16)]
        public string? BankCardNumber { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Order>? Orders { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

    }
}
