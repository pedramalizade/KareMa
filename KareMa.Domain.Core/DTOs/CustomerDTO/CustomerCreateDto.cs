using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomerCreateDto
{
    public string FirstName { get; set; }
    [DisplayName("نام خانوادگی")]
    public string LastName { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum? Gender { get; set; }
    [DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }
    [DisplayName("شماره تلفن ذخیره")]
    public string BackUpPhoneNumber { get; set; }
    [DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; }
    public Address Addresses { get; set; }
}