using System.ComponentModel.DataAnnotations;

namespace KareMa.Domain.Core.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "در انتظار پیشنهاد متخصصان")]
        AwaitingSuggestionExperts ,
        [Display(Name = "در انتظار تایید مشتری")]
        AwaitingCustomerConfirmation ,
        [Display(Name = "تایید شد")]
        Confirmed ,
        [Display(Name = "تایید نشد")]
        NotConfirmed,
        [Display(Name = "انجام شد")]
        Done 
    }
}
