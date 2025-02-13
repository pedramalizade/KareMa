using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "خانوم")]
        Female = 1,
        [Display(Name = "آقا")]
        Male
    }

}
