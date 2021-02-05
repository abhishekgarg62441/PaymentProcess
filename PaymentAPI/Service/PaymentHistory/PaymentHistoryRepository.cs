using Microsoft.AspNetCore.Http;
using PaymentAPI.Context;
using PaymentAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe.Infrastructure;
using Stripe;
using PayPal.Api;
using PaymentAPI.Helpers;
using Microsoft.Extensions.Options;

namespace PaymentAPI.Service.PaymentHistory
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        readonly ProcessPaymentsContext _context;

        public static object PayPalClient { get; private set; }

        private readonly AppSettings _appSettings;
        public PaymentHistoryRepository(ProcessPaymentsContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public async Task<stringMessage> ProcessPayment(PaymentSetting Setting)
        {
            try
            {

                //ProcessPaymentDetail item = new ProcessPaymentDetail();

                //item.CardHolder = Setting.CardHolder;
                //item.CreditCardNumber = Setting.CreditCardNumber;
                //item.ExpirationDate = Setting.ExpirationDate;
                //item.SecurityCode = Setting.SecurityCode;
                //item.Amount = Setting.Amount;

                //await _context.ProcessPaymentDetail.AddAsync(item);
                if (Setting.Amount < 20)
                {
                    StripeConfiguration.ApiKey = "sk_test_51IHDivDPPCmSyJa9ukQe2ZG9ECidOcSRThPKlZZD2pOubjlF8RnSgm4lrkKtYFt9Ai6WIvdM2KWlDfHy5KFwPBXV00FfylM2Ri";

                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = Convert.ToInt64(Setting.Amount),
                        Currency = "inr",
                        PaymentMethodTypes = new List<string>
  {
    "card",
  },
                        ReceiptEmail = "abhishek@brucode.com",
                    };

                    var service = new PaymentIntentService();
                    var paymentIntent = service.Create(options);

                }

                else if (Setting.Amount > 20 & Setting.Amount < 500)
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("mode", _appSettings.mode);
                    dict.Add("connectionTimeout", _appSettings.connectionTimeout);
                    dict.Add("requestRetries", _appSettings.requestRetries);
                    dict.Add("paypal", _appSettings.paypal);

                    string accessToken = new OAuthTokenCredential(_appSettings.ClientID, _appSettings.Secret, dict).GetAccessToken();
                    var apiContext = new APIContext(accessToken);

                    var transaction = new Transaction()
                    {
                        amount = new Amount()
                        {
                            currency = "GBP",
                            total = Setting.Amount.ToString("N"),
                            details = new Details()
                            {
                                shipping = "0",
                                subtotal = Convert.ToString(Setting.Amount),
                                tax = "0"
                            }
                        },
                        description = "Online Payment",
                        item_list = new ItemList()
                        {

                            items = new List<Item>()
                            {
                            },
                            shipping_address = new ShippingAddress
                            {
                                city = "",
                                country_code = "GB",
                                line1 = "",
                                postal_code = "",
                                state = "",
                                recipient_name = ""
                            }
                        },
                        invoice_number = (new Random().Next(999999).ToString())
                    };


                    int month = 0, year = 0;
                    int.TryParse(Setting.ExpirationDate.Month.ToString(), out month);
                    int.TryParse(Setting.ExpirationDate.Year.ToString(), out year);

                    var CardType = "visa";


                    var payer = new Payer()
                    {
                        payment_method = "credit_card",
                        funding_instruments = new List<FundingInstrument>()
                {
                    new FundingInstrument()
                    {
                        credit_card = new CreditCard()
                        {
                            billing_address = new PayPal.Api.Address()
                            {

                            city = "",
                            country_code = "GB",
                            line1 = "",
                            postal_code = "RH19 1EQ",
                            state = "",


                            },
                            cvv2 = Setting.SecurityCode,
                            expire_month = month,
                            expire_year = year,
                            first_name = Setting.CardHolder,
                            last_name = "",
                            number = Setting.CreditCardNumber,
                            type = CardType
                        }
                    }
                },
                        payer_info = new PayerInfo
                        {
                            email = "abhishekgarg62441@gmail.com"
                        }
                    };

                    var payment = new Payment()
                    {
                        intent = "sale",
                        payer = payer,
                        transactions = new List<Transaction>() { transaction }
                    };


                    try
                    {
                        var createdPayment = payment.Create(apiContext);


                        if (createdPayment.state == "approved")
                        {
                            var id = createdPayment.id;
                        }
                    }
                    catch (Exception ex)
                    {
                        return new stringMessage(" * **Unable to process card.Please verify card details and try again", "Failed");

                    }


                }






                await _context.SaveChangesAsync();



                return new stringMessage("Payment Successfully done.", "Success");

            }
            catch (Exception ex)
            {
                //await _iLogRepository.AddLog(ex.StackTrace + " --- " + ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : ""), "APILOG");
                return new stringMessage("Exception", "Failed");
            }
        }



        //public CardPaymentReceiptViewModel Create(CardPaymentViewModel viewModel)
        //{
        //    var paymentTransactionId = Guid.NewGuid().ToString();

        //    var chargeCreateOptions = new ChargeCreateOptions
        //    {
        //        TransferGroup = paymentTransactionId,
        //        Amount = PaymentAmountPence,
        //        Currency = PaymentCurrency,
        //        Description = PaymentDescription,
        //        SourceId = viewModel.Token,
        //        Capture = CaptureCardPayment,
        //        ReceiptEmail = viewModel.Email,
        //    };

        //    var charge = _chargeService.Create(chargeCreateOptions);

        //    return ToPaymentReceipt(charge);
        //}

        //private CardPaymentReceiptViewModel ToPaymentReceipt(Charge charge)
        //{
        //    var cardPaymentReceiptViewModel = new CardPaymentReceiptViewModel
        //    {
        //        Amount = charge.Amount,
        //        Currency = charge.Currency,
        //        Description = charge.Description,
        //        Status = charge.Status,
        //        Created = charge.Created,
        //        BalanceTransactionId = charge.BalanceTransactionId,
        //        Id = charge.Id,
        //        SourceId = charge.Source.Id,

        //    };

        //    return cardPaymentReceiptViewModel;
        //}
    }
    public class CardPaymentViewModel
    {
        public string Email { get; set; }

        public string Token { get; set; }
    }
    public class CardPaymentReceiptViewModel
    {
        public long Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime Created { get; set; }

        public string BalanceTransactionId { get; set; }

        public string Id { get; set; }

        public string SourceId { get; set; }
    }
}
