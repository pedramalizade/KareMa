using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetCommentsDto
{
    public int Id { get; set; }
    public string ExpertName { get; set; }
    public string ExpertFamily { get; set; }
    public int ExpertId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerFamily { get; set; }
    public int CustomerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int Score { get; set; }
}
