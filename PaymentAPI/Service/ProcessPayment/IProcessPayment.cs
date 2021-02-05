using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Service.ProcessPayment
{
    public interface IProcessPayment
    {
         Task<stringMessage> CardProcessPayment(PaymentModel Model);
    }
}
