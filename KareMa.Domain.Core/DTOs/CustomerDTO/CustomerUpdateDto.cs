using KareMa.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Enums;

public class CustomerUpdateDto
{
    public int Id { get; set; }
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
    public string? BankCardNumber { get; set; }
}