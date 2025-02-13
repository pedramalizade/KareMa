using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddressCreateDto
{
    [DisplayName("عنوان")]
    public string Title { get; set; }
    public int CityId { get; set; }
    [DisplayName("شهر")]
    public City City { get; set; }
    [DisplayName("خیابان")]
    public string Street { get; set; }
    [DisplayName("منطقه و کوچه و آپارتمان و ...")]
    public string Area { get; set; }
    [DisplayName("کدپستی")]
    public string PostalCode { get; set; }
    [DisplayName("پیش فرض")]
    public bool IsDefault { get; set; }
}