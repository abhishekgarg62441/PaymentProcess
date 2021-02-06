using PaymentAPI.DTO;
using System.Threading.Tasks;

namespace PaymentAPI.Service.PaymentDomain
{
    public interface IPremiumPaymentGateway
    {
        string CreatePayment(PaymentModel model);
    }
}
