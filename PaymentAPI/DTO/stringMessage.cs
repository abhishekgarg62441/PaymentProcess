using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.DTO
{
    public class StringMessage
    {
        public string Message { get; set; }
        public string Response { get; set; }
        public StringMessage(string _Message, string _Response)
        {
            Message = _Message;
            Response = _Response;
        }
    }
}
