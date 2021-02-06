using System;
using PaymentAPI.DTO;
using System.Threading.Tasks;
using PaymentAPI.Enums;

namespace PaymentAPI.Service.PaymentDomain
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        /// <summary>
        /// To Process Payment Using Expensive Payment Service. 
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <returns>
        /// It returns the status based on payment processed from gateway.
        /// </returns>
        public string CreatePayment(PaymentModel paymentModel)
        {
            try
            {
                //code to process payment online expensive payment implemnetation
                return PaymentStatus.Processed.ToString();
            }
            catch (Exception ex)
            {
                return PaymentStatus.Failed.ToString();
            }
        }
    }
}
