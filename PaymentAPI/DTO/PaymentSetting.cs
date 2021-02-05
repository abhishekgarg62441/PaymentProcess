using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentAPI.DTO
{
    public class PaymentModel
    {
        [Required(ErrorMessage = "Please enter card number.")]

        //[Range(100000000000, 9999999999999999999, ErrorMessage = "CCN must be between 12 and 19 digits")]
        [CreditCard(AcceptedCardTypes = CreditCardAttribute.CardType.Visa | CreditCardAttribute.CardType.MasterCard,ErrorMessage ="Please enter valid CCN.")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Please enter card holder name.")]
        public string CardHolder { get; set; }
       
        [Required(ErrorMessage = "Please enter expiration date.")]
        [DataType(DataType.Date,ErrorMessage ="Incorrect date format.")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3, ErrorMessage = "Security code should be 3 chracters long.", MinimumLength = 0)]
        public string SecurityCode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        [DataType(DataType.Currency)]
        [Column(TypeName  = "decimal(18, 2)")]
        public double Amount { get; set; }
    }
    
    public enum PaymentStatus
    {
        pending = 1,
        processed = 2,
        failed = 3
    }

    public class CreditCardAttribute : ValidationAttribute
    {
        private CardType _cardTypes;
        public CardType AcceptedCardTypes
        {
            get { return _cardTypes; }
            set { _cardTypes = value; }
        }

        public CreditCardAttribute()
        {
            _cardTypes = CardType.All;
        }

        public CreditCardAttribute(CardType AcceptedCardTypes)
        {
            _cardTypes = AcceptedCardTypes;
        }

        public override bool IsValid(object value)
        {
            var number = Convert.ToString(value);

            if (String.IsNullOrEmpty(number))
                return true;

            return IsValidType(number, _cardTypes) && IsValidNumber(number);
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field contains an invalid credit card number.";
        }

        public enum CardType
        {
            Unknown = 1,
            Visa = 2,
            MasterCard = 4,
            Amex = 8,
            Diners = 16,

            All = CardType.Visa | CardType.MasterCard | CardType.Amex | CardType.Diners,
            AllOrUnknown = CardType.Unknown | CardType.Visa | CardType.MasterCard | CardType.Amex | CardType.Diners
        }

        private bool IsValidType(string cardNumber, CardType cardType)
        {
            // Visa
            if (Regex.IsMatch(cardNumber, "^(4)")
                && ((cardType & CardType.Visa) != 0))
                return cardNumber.Length == 13 || cardNumber.Length == 16;

            // MasterCard
            if (Regex.IsMatch(cardNumber, "^(51|52|53|54|55)")
                && ((cardType & CardType.MasterCard) != 0))
                return cardNumber.Length == 16;

            // Amex
            if (Regex.IsMatch(cardNumber, "^(34|37)")
                && ((cardType & CardType.Amex) != 0))
                return cardNumber.Length == 15;

            // Diners
            if (Regex.IsMatch(cardNumber, "^(300|301|302|303|304|305|36|38)")
                && ((cardType & CardType.Diners) != 0))
                return cardNumber.Length == 14;

            //Unknown
            if ((cardType & CardType.Unknown) != 0)
                return true;

            return false;
        }

        private bool IsValidNumber(string number)
        {
            int[] DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0;
            char[] chars = number.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = ((int)chars[i]) - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS[j];
            }

            return ((checksum % 10) == 0);
        }
    }
}
