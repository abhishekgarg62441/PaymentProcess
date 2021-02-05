using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public class CheapPaymentGateway : ICheapPaymentGeteway
    {
        public async Task<string> CreatePayment(PaymentModel model)
        {
            try
            {
                //code to process payment online cheappayment
                return PaymentStatus.processed.ToString();
            }
            catch (Exception ex)
            {
                return PaymentStatus.failed.ToString();
            }
        }
    }
}
