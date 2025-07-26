using System.ComponentModel.DataAnnotations;

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
