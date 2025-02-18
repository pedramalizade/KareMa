using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
