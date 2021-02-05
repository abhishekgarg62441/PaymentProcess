using Microsoft.AspNetCore.Http;
using PaymentAPI.Context;
using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaymentAPI.Service.PaymentDomain;

namespace PaymentAPI.Service.ProcessPayment
{
    public class ProcessPayment : IProcessPayment
    {
        readonly ProcessPaymentsContext _context;

        private readonly ICheapPaymentGeteway _iCheapPaymentGeteway;
        private readonly IExpensivePaymentGateway _iExpensivePaymentGateway;
        public ProcessPayment(ProcessPaymentsContext context, ICheapPaymentGeteway iCheapPaymentGeteway, IExpensivePaymentGateway iExpensivePaymentGateway)
        {
            _context = context;
            _iCheapPaymentGeteway = iCheapPaymentGeteway;
            _iExpensivePaymentGateway = iExpensivePaymentGateway;
        }
        public async Task<stringMessage> CardProcessPayment(PaymentModel model)
        {
            try
            {

                if (model.Amount < 20)
                {
                    var status = await _iCheapPaymentGeteway.CreatePayment(model);
                    return await SaveDetail(model, status);
                }

                else if (model.Amount > 20 & model.Amount < 500)
                {
                    var status = await _iExpensivePaymentGateway.CreatePayment(model);

                    return await SaveDetail(model, status);
                }
                else
                {
                    //yet to work  //need to retry 3 times
                    string status = "Processed";

                    return await SaveDetail(model, status);
                }



            }
            catch (Exception ex)
            {
                return new stringMessage("Exception", "Failed");
            }
        }

        private async Task<stringMessage> SaveDetail(PaymentModel model, string status)
        {
            ProcessPaymentDetail payment = new ProcessPaymentDetail();

            payment.CardHolder = model.CardHolder;
            payment.CreditCardNumber = model.CreditCardNumber;
            payment.ExpirationDate = model.ExpirationDate;
            payment.SecurityCode = model.SecurityCode;
            payment.Amount = model.Amount;

            await _context.ProcessPaymentDetail.AddAsync(payment);
            await _context.SaveChangesAsync();

            PaymentState paymentstate = new PaymentState();
            paymentstate.PaymentID = payment.PaymentID;
            paymentstate.PaymentStateStatus = status;


            await _context.PaymentState.AddAsync(paymentstate);
            await _context.SaveChangesAsync();



            return new stringMessage("Payment is processedt.", "Success");
        }
    }

}
