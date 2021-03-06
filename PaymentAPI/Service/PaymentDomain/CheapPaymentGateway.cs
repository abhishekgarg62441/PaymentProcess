﻿using PaymentAPI.DTO;
using PaymentAPI.Enums;
using System;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        /// <summary>
        /// To Process Payment Using Cheap Payment Service. 
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <returns>
        /// It returns the status based on payment processed from gateway.
        /// </returns>
        public string CreatePayment(PaymentModel paymentModel)
        {
            try
            {
                //code to process payment online cheappayment
                return PaymentStatus.Processed.ToString();
            }
            catch (Exception ex)
            {
                return PaymentStatus.Failed.ToString();
            }
        }
    }
}
