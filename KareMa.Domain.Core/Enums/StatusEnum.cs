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
        AwaitingSuggestionExperts,
        [Display(Name = "در انتظار تایید مشتری")]
        AwaitingCustomerConfirmation,
        [Display(Name = "تایید شد")]
        Confirmed,

        //[Display(Name = "تایید نشد")]
        //NotConfirmed,
        //[Display(Name = "در انتظار پرداخت")]
        //AwaitingPayment,
        //[Display(Name = "پرداخت شد")]
        //Paied,
        //[Display(Name = "انجام شد")]
        //Done,
        //[Display(Name = "لغو شد")]
        //Canceled
    }
}
