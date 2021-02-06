using System;
using PaymentAPI.DTO;
using System.Threading.Tasks;
using PaymentAPI.Enums;

namespace PaymentAPI.Service.PaymentDomain
{
    public class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        /// <summary>
        /// To Process Payment Using Premium Payment Service. 
        /// </summary>
        /// <param name="paymentModel">PaymentModel</param>
        /// <returns>
        /// It returns the status based on payment process.
        /// </returns>
        public string CreatePayment(PaymentModel paymentModel)
        {
            try
            {
                //Code Here To Call Premium Payment Geteway
                return PaymentStatus.Processed.ToString();
            }
            catch (Exception ex)
            {
                return PaymentStatus.Failed.ToString();
            }
        }
    }
}
