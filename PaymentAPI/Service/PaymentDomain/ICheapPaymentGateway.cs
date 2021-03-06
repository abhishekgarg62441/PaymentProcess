﻿using PaymentAPI.DTO;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public interface ICheapPaymentGateway
    {
       string CreatePayment(PaymentModel paymentModel);
    }
}
