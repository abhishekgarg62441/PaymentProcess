﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentAPI.DTO;
using PaymentAPI.Service.PaymentHistory;

namespace PaymentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IConfiguration _iConfiguration;

        private readonly IPaymentHistoryRepository _iPaymentHistoryRepository;
        public PaymentController( IConfiguration iConfiguration, IPaymentHistoryRepository iPaymentHistoryRepository)
        {
            _iConfiguration = iConfiguration;
            _iPaymentHistoryRepository = iPaymentHistoryRepository;

        }

        [AllowAnonymous]
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment(PaymentSetting Setting)
        {
            try
            {
                if (Setting == null)
                {
                    return BadRequest("Invalid request");
                }
                var response = await _iPaymentHistoryRepository.ProcessPayment(Setting);

                return Ok((new { Message = response.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}