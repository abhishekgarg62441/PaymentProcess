using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.DTO
{
    public class PaymentSetting
    {
        [Required(ErrorMessage = "required")]
        [Range(100000000000, 9999999999999999999, ErrorMessage = "must be between 12 and 19 digits")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "required")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "required")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        
        [Required(ErrorMessage = "required")]
        public double Amount { get; set; }
    }

    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    //public sealed class CreditCardAttribute : DataTypeAttribute
    //{
    //    public CreditCardAttribute() : base(DataType.CreditCard)
    //    {
    //        base.ErrorMessage = DataAnnotationsResources.CreditCardAttribute_Invalid;
    //    }
    //    public override bool IsValid(object value)
    //    {
    //        if (value == null)
    //        {
    //            return true;
    //        }
    //        string text = value as string;
    //        if (text == null)
    //        {
    //            return false;
    //        }
    //        text = text.Replace("-", "");
    //        text = text.Replace(" ", "");
    //        int num = 0;
    //        bool flag = false;
    //        foreach (char current in text.Reverse<char>())
    //        {
    //            if (current < '0' || current > '9')
    //            {
    //                return false;
    //            }
    //            int i = (int)((current - '0') * (flag ? '\u0002' : '\u0001'));
    //            flag = !flag;
    //            while (i > 0)
    //            {
    //                num += i % 10;
    //                i /= 10;
    //            }
    //        }
    //        return num % 10 == 0;
    //    }
    //}
}
