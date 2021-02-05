using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Context
{
    public class PaymentState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentStateID { get; set; }
        public string PaymentStateStatus { get; set; }
        
        
        public int? PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public virtual ProcessPaymentDetail ProcessPaymentDetail { get; set; }


    }
}
