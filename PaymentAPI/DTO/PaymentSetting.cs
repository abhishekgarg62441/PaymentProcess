using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.DTO
{
    public class PaymentModel
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

    public enum PaymentStatus
    {
        pending = 1,
        processed = 2,
        failed = 3
    }
}
