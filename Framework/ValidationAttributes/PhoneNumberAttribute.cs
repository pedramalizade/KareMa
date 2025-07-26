using System.ComponentModel.DataAnnotations;

namespace Framework.ValidationAttributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string phoneNumber)
            {
                return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.StartsWith("09");
            }
            return false;
        }
    }
}
