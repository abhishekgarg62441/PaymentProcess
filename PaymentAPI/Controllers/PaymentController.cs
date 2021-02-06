using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.DTO;
using PaymentAPI.Service.ProcessPayment;

namespace PaymentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IProcessPayment _iPaymentHistoryRepository;
        public PaymentController(IProcessPayment iPaymentHistoryRepository)
        {
            _iPaymentHistoryRepository = iPaymentHistoryRepository;

        }


        /// <summary>
        /// To Process Payment.
        /// </summary>
        /// <param name="paymentModel">Payment Model</param>
        /// <returns>
        /// It returns the payment status for the requested payment.
        /// or 
        /// It returns customized error if any exception occur.
        /// </returns>
        [AllowAnonymous,HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment(PaymentModel paymentModel)
        {
            try
            {
                if (paymentModel == null)
                {
                    return BadRequest("Invalid request");
                }

                var response = await _iPaymentHistoryRepository.CardProcessPayment(paymentModel);

                if (response != null)
                {
                    if (response.Response == "Failed")
                    {
                        return StatusCode(500, "Internal server error");
                    }
                }

                return Ok((new { Message = response.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
