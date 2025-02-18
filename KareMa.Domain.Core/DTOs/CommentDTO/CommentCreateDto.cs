using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KareMa.Domain.Core.Entities;

public class CommentCreateDto
{
    public string Title { get; set; }
    [DisplayName("متن کامنت")]
    public string Description { get; set; }
    [DisplayName("رضایت")]
    public int Score { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
}