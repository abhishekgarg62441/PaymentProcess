using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Helpers
{
    public class AppSettings
    {
        public string ClientID { get; set; }
        public string Secret { get; set; }
        public string entityFramework { get; set; }
        public string paypal { get; set; }
        public string log4net { get; set; }
        public string mode { get; set; }
        public string connectionTimeout { get; set; }
        public string requestRetries { get; set; }
        
    }
}
