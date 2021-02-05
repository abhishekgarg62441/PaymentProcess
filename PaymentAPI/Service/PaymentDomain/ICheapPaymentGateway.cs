using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public interface ICheapPaymentGeteway
    {
        Task<PaymentStatus> CreatePayment(PaymentModel model);
    }
}
