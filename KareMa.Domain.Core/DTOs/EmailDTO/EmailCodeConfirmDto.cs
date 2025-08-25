using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.DTOs.EmailDTO
{
    public class EmailCodeConfirmDto
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
