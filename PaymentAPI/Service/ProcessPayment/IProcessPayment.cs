using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.ProcessPayment
{
    public interface IProcessPayment
    {
        /// <summary>
        /// To Process Payment.
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <returns>
        /// It returns the payment status for the requested process.
        /// or 
        /// It returns customized error if any exception occur.
        /// </returns>
        Task<StringMessage> CardProcessPayment(PaymentModel paymentModel);
    }
}
