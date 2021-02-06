using System;
using PaymentAPI.DTO;
using PaymentAPI.Context;
using System.Threading.Tasks;
using PaymentAPI.Service.PaymentDomain;
using System.Collections.Generic;
using PaymentAPI.Enums;

namespace PaymentAPI.Service.ProcessPayment
{
    public class ProcessPayment : IProcessPayment
    {
        readonly ProcessPaymentsContext _context;
        private readonly ICheapPaymentGateway _iCheapPaymentGeteway;
        private readonly IExpensivePaymentGateway _iExpensivePaymentGateway;
        private readonly IPremiumPaymentGateway _iPremiumPaymentGeteway;
        private static int paymentProcessRetry = 0;
        private List<string> paymentGateways = new List<string> { "Cheap", "Expensive", "Premium" };
        public ProcessPayment(ProcessPaymentsContext context, ICheapPaymentGateway iCheapPaymentGeteway, IExpensivePaymentGateway iExpensivePaymentGateway, IPremiumPaymentGateway iPremiumPaymentGeteway)
        {
            _context = context;
            _iCheapPaymentGeteway = iCheapPaymentGeteway;
            _iExpensivePaymentGateway = iExpensivePaymentGateway;
            _iPremiumPaymentGeteway = iPremiumPaymentGeteway;
        }

        /// <summary>
        /// To process payment and decide the payment provider based on amount.
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <returns>
        /// It returns the payment status for the requested process.
        /// or 
        /// It returns customized error if any exception occur.
        /// </returns>
        public async Task<StringMessage> CardProcessPayment(PaymentModel paymentModel)
        {
            try
            {
                string paymentStatus = string.Empty;
                paymentProcessRetry = 0; 
                paymentStatus = await SendPaymentProcessRequest(paymentModel, paymentStatus);
                return await SavePaymentDetails(paymentModel, paymentStatus);

            }
            catch (Exception ex)
            {
                return new StringMessage("Exception", "Failed");
            }
        }

        private async Task<string> SendPaymentProcessRequest(PaymentModel paymentModel, string paymentStatus)
        {
            //Check For Cheap Payment Service

            if (paymentModel.Amount <= 20)
            {
                paymentStatus =  _iCheapPaymentGeteway.CreatePayment(paymentModel);
            }

            //Check For Expensive Payment Service
            else if (paymentModel.Amount > 20 & paymentModel.Amount <= 500)
            {
                if (paymentGateways.Contains(PaymentGateways.Expensive.ToString()))
                {
                    paymentStatus =  _iExpensivePaymentGateway.CreatePayment(paymentModel);
                }
                else
                {
                    paymentStatus =  _iCheapPaymentGeteway.CreatePayment(paymentModel);
                }
            }

            //Check For Premium Payment Service
            else if (paymentModel.Amount > 500)
            {
                paymentStatus =  _iPremiumPaymentGeteway.CreatePayment(paymentModel);
                if (paymentStatus == PaymentStatus.Failed.ToString() && paymentProcessRetry < 3)
                {
                    paymentProcessRetry++;
                    await SendPaymentProcessRequest(paymentModel, paymentStatus);
                }
            }

            return paymentStatus;
        }

        /// <summary>
        /// To save payment details.
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <param name="paymentStatus">Payment Status</param>
        /// <returns>
        /// It returns the payment status for the requested process.
        /// or 
        /// It returns customized error if any exception occur.
        /// </returns>
        private async Task<StringMessage> SavePaymentDetails(PaymentModel paymentModel, string paymentStatus)
        {
            try
            {
                ProcessPaymentDetail payment = new ProcessPaymentDetail();
                payment.CardHolder = paymentModel.CardHolder;
                payment.CreditCardNumber = paymentModel.CreditCardNumber;
                payment.ExpirationDate = paymentModel.ExpirationDate;
                payment.SecurityCode = paymentModel.SecurityCode;
                payment.Amount = paymentModel.Amount;
                await _context.ProcessPaymentDetail.AddAsync(payment);
                await _context.SaveChangesAsync();

                PaymentState paymentstate = new PaymentState();
                paymentstate.PaymentID = payment.PaymentID;
                paymentstate.PaymentStateStatus = paymentStatus;
                await _context.PaymentState.AddAsync(paymentstate);
                await _context.SaveChangesAsync();
                return new StringMessage("Payment is processed.", paymentStatus);
            }
            catch (Exception e)
            {
                return new StringMessage(e.Message, "Failed");
            }

        }
    }

}
