using Microsoft.AspNetCore.Http;
using PaymentAPI.Context;
using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaymentAPI.Service.PaymentDomain;

namespace PaymentAPI.Service.PaymentHistory
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        readonly ProcessPaymentsContext _context;

        private readonly ICheapPaymentGeteway _iCheapPaymentGeteway;
        private readonly IExpensivePaymentGateway _iExpensivePaymentGateway;
        public PaymentHistoryRepository(ProcessPaymentsContext context, ICheapPaymentGeteway iCheapPaymentGeteway, IExpensivePaymentGateway iExpensivePaymentGateway)
        {
            _context = context;
            _iCheapPaymentGeteway = iCheapPaymentGeteway;
            _iExpensivePaymentGateway = iExpensivePaymentGateway;
        }
        public async Task<stringMessage> ProcessPayment(PaymentModel model)
        {
            try
            {

                if (model.Amount < 20)
                {
                    var status = await _iCheapPaymentGeteway.CreatePayment(model);
                    await _context.SaveChangesAsync();



                    return new stringMessage("Payment Successfully done.", "Success");
                }

                else if (model.Amount > 20 & model.Amount < 500)
                {
                    var status = await _iExpensivePaymentGateway.CreatePayment(model);


                    ProcessPaymentDetail item = new ProcessPaymentDetail();

                    item.CardHolder = model.CardHolder;
                    item.CreditCardNumber = model.CreditCardNumber;
                    item.ExpirationDate = model.ExpirationDate;
                    item.SecurityCode = model.SecurityCode;
                    item.Amount = model.Amount;

                    await _context.ProcessPaymentDetail.AddAsync(item);
                    await _context.SaveChangesAsync();



                    return new stringMessage("Payment Successfully done.", "Success");
                }
                else
                {
                    await _context.SaveChangesAsync();



                    return new stringMessage("Payment Successfully done.", "Success");
                }

               

            }
            catch (Exception ex)
            {
                return new stringMessage("Exception", "Failed");
            }
        }




    }

}
