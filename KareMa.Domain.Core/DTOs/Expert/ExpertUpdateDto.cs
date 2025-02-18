using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;

public class ExpertUpdateDto
{
    public int Id { get; set; }
    [DisplayName("نام")]
    public string FirstName { get; set; }
    [DisplayName("نام خانوداگی")]
    public string LastName { get; set; }
    [DisplayName("جنسیت")]
    public GenderEnum Gender { get; set; }
    [DisplayName("شماره تلفن")]
    public string PhoneNumber { get; set; }
    [DisplayName("تاریخ تولد")]
    public DateTime BirthDate { get; set; }
    [DisplayName("عکس پروفایل")]
    public string ProfileImage { get; set; }
    [DisplayName("شماره کارت بانکی")]
    public string BankCardNumber { get; set; }
    [DisplayName("آدرس")]
    public Address Address { get; set; }
    public List<Service> Services { get; set; }


}