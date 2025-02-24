using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.CustomerDTO
{
    public class CustomerSummaryDto
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public GenderEnum? Gender { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(16)]
        public string? BankCardNumber { get; set; }
        public Address Addresses { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
