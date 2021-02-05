using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.DTO
{
    public class stringMessage
    {
        public string Message { get; set; }
        public string Response { get; set; }
        public stringMessage(string _Message, string _Response)
        {
            Message = _Message;
            Response = _Response;
        }
    }
}
