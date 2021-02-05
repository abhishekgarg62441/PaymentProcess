using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        public async Task<string> CreatePayment(PaymentModel model)
        {
            try
            {
                //code to process payment online expensive payment implemnetation
                return PaymentStatus.processed.ToString();
            }
            catch (Exception ex)
            {
                return PaymentStatus.failed.ToString();
            }
        }
    }
}
