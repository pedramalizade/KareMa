using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Domain.Core.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "در انتظار پیشنهاد متخصصان")]
        AwaitingSuggestionExperts = 1,
        [Display(Name = "در انتظار تایید مشتری")]
        AwaitingCustomerConfirmation = 2,
        [Display(Name = "تایید شد")]
        Confirmed = 3,
        [Display(Name = "تایید نشد")]
        NotConfirmed,
        [Display(Name = "انجام شد")]
        Done,
    }
}
