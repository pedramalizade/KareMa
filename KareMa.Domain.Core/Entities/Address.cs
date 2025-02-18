using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Entities
{
    public class Address
    {
        public Address()
        {
            //SetFullAddress();
            FullAddress = City.Name + Street + Area;
        }
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? ExpertId { get; set; }
        public Expert? Expert { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public int CityId { get; set; }
        [MaxLength(50)]
        public City? City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(50)]
        public string Area { get; set; }
        [MaxLength(1000)]
        public string? FullAddress { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public void SetFullAddress()
        //{
        //    FullAddress = City.Name + Street + Area;
        //}

        //public Address() { }
        //public int Id { get; set; }
        //public Customer? Customer { get; set; }
        //public int? CustomerId { get; set; }
        //public Expert? Expert { get; set; }
        //public int? ExpertId { get; set; }
        //public int? CityId { get; set; }
        //[MaxLength(50)]
        //public City? City { get; set; }
        //[MaxLength(50, ErrorMessage = "نام خیابان  نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        //[MinLength(2, ErrorMessage = "نام خیابان نمی‌تواند کمتر از 2 کاراکتر باشد")]
        //[Required(ErrorMessage = "نام خیابان نمی‌تواند بدون مقدار باشد")]
        //public string? Street { get; set; }
        //[MaxLength(500, ErrorMessage = "نام محله نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        //[MinLength(3, ErrorMessage = "نام محله نمی‌تواند کمتر از 3 کاراکتر باشد")]
        //[Required(ErrorMessage = "نام محله نمی‌تواند بدون مقدار باشد")]
        //public string? Area { get; set; }
        //[DisplayName("کدپستی")]
        //[Length(10, 10, ErrorMessage = "کدپستی نمی‌تواند کمتر یا بیشتر از 10 کاراکتر باشد")]
        //[Required(ErrorMessage = "کدپستی نمی‌تواند بدون مقدار باشد")]
        //public string? PostalCode { get; set; }
        //public bool IsDeleted { get; set; } = false;
        //public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
