using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerIntegrationApp.Models
{
    public class CustomerCardValidation
    {
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public long Token { get; set; }
        public int CVV { get; set; }
    }
}
