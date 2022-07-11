using CustomerIntegrationApp.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerIntegrationApp.Models
{
    public class CustomerCardRegistration
    {
        public DateTime RegistrationDate { get; set; }
        public long Token { get; set; }
        public int CardId { get; set; }

        internal CustomerCardRegistration Factory(CustomerCard message)
        {
            CustomerCardRegistration customerCardRegistration = new CustomerCardRegistration();

            var cvvList = message.CCV.ToString().Select(t => int.Parse(t.ToString())).ToList();
            var cardNumberList = message.CardNumber.ToString().Select(t => int.Parse(t.ToString())).ToList();

            if (cvvList.Count() > 2 && cvvList.Count() < 6)
            {
                if (message.CustomerId > 0)
                {
                    if (cardNumberList.Count() > 0 && cardNumberList.Count() < 17)
                    {
                        customerCardRegistration.CardId = message.CustomerId + new Random(message.CustomerId).Next();
                        customerCardRegistration.RegistrationDate = DateTime.Now;
                        customerCardRegistration.Token = SecurityHelper.GenerateToken(message.CCV, message.CardNumber, customerCardRegistration.RegistrationDate);
                    }
                    else
                    {
                        throw new Exception("CardNumber is invalid.");
                    }
                }
                else
                {
                    throw new Exception("CustomerId is invalid.");
                }
            }
            else
            {
                throw new Exception("CCV is invalid.");
            }

            return customerCardRegistration;
        }
    }
}
