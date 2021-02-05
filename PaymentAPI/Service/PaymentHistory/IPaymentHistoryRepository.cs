using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentHistory
{
    public interface IPaymentHistoryRepository
    {
         Task<stringMessage> ProcessPayment(PaymentModel Model);
    }
}
