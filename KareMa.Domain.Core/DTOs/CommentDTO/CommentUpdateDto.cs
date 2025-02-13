using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommentUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    [DisplayName("متن کامنت")]
    public string Description { get; set; }
    [DisplayName("رضایت")]
    public int Score { get; set; }
}