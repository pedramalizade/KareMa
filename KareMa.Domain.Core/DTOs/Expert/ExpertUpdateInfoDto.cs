using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExpertUpdateInfoDto
{
    public int Id { get; set; }

    [MaxLength(20)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    public GenderEnum? Gender { get; set; }
    [MaxLength(11)]
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ProfileImage { get; set; }
    public List<Service>? Services { get; set; }
}
