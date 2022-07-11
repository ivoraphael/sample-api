using System;

namespace CustomerIntegrationApp.Models
{
    public class CustomerCard
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CCV { get; set; }
    }
}
