﻿using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public class CheapPaymentGateway : ICheapPaymentGeteway
    {
        public Task<PaymentStatus> CreatePayment(PaymentModel model)
        {
            throw new NotImplementedException();
        }
    }
}