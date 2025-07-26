using KareMa.Domain.Core.Entities;
using System.ComponentModel;

public class AddressUpdateDto
{
    public int Id { get; set; }
    public int CityId { get; set; }
    [DisplayName("شهر")]
    public City City { get; set; }
    [DisplayName("خیابان")]
    public string Street { get; private set; }
    [DisplayName("منطقه و کوچه و آپارتمان و ...")]
    public string Area { get; set; }
    [DisplayName("کدپستی")]
    public string PostalCode { get; set; }
    [DisplayName("پیش فرض")]
    public bool IsDefault { get; set; }
}