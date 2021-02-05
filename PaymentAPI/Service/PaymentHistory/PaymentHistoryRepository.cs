using Microsoft.AspNetCore.Http;
using PaymentAPI.Context;
using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe.Infrastructure;
using Stripe;
using PayPal.Api;
using Microsoft.Extensions.Options;

namespace PaymentAPI.Service.PaymentHistory
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        readonly ProcessPaymentsContext _context;

      
        public PaymentHistoryRepository(ProcessPaymentsContext context)
        {
            _context = context;
            
        }
        public async Task<stringMessage> ProcessPayment(PaymentSetting Setting)
        {
            try
            {

                //ProcessPaymentDetail item = new ProcessPaymentDetail();

                //item.CardHolder = Setting.CardHolder;
                //item.CreditCardNumber = Setting.CreditCardNumber;
                //item.ExpirationDate = Setting.ExpirationDate;
                //item.SecurityCode = Setting.SecurityCode;
                //item.Amount = Setting.Amount;

                //await _context.ProcessPaymentDetail.AddAsync(item);
                if (Setting.Amount < 20)
                {
                   

                }

                else if (Setting.Amount > 20 & Setting.Amount < 500)
                {
                  

                }






                await _context.SaveChangesAsync();



                return new stringMessage("Payment Successfully done.", "Success");

            }
            catch (Exception ex)
            {
                //await _iLogRepository.AddLog(ex.StackTrace + " --- " + ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : ""), "APILOG");
                return new stringMessage("Exception", "Failed");
            }
        }



    }
 
}
