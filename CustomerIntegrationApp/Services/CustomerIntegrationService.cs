using CustomerIntegrationApp.Data.Utils;
using CustomerIntegrationApp.Models;
using CustomerIntegrationApp.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerIntegrationApp.Services
{
    public interface ICustomerIntegrationService
    {
        CustomerCardRegistration Registrate(CustomerCard message);
        bool Validate(CustomerCardValidation message);
    }

    public class CustomerIntegrationService : ICustomerIntegrationService
    {
        private readonly ICustomerIntegrationRepository _repository;

        public CustomerIntegrationService(IConfiguration configuration, ICustomerIntegrationRepository repository)
        {
            _repository = repository;
        }

        public CustomerCardRegistration Registrate(CustomerCard message) 
        {
            List<CustomerCard> customerCards = _repository.GetCustomerCards();

            if (customerCards.Where(x => x.CustomerId == message.CustomerId).Count() > 0)
            {
                message = customerCards.Where(x => x.CustomerId == message.CustomerId).FirstOrDefault();
                CustomerCardRegistration customerCardRegistration = new CustomerCardRegistration().Factory(message);
                _repository.SaveRegistration(customerCardRegistration);
                return customerCardRegistration;
            }
            else
            {
                CustomerCardRegistration customerCardRegistration = new CustomerCardRegistration().Factory(message);
                _repository.SaveCustomerCard(message);
                _repository.SaveRegistration(customerCardRegistration);
                return customerCardRegistration;
            }
        }

        public bool Validate(CustomerCardValidation message)
        {
            bool result = false;

            List<CustomerCard> customerCards = _repository.GetCustomerCards();
            List<CustomerCardRegistration> customerCardRegistrations = _repository.GetRegistrations().OrderByDescending(x => x.RegistrationDate).ToList();

            if (customerCards.Count() > 0)
            {
                CustomerCard customerCard = customerCards.Where(x => x.CustomerId == message.CustomerId).FirstOrDefault();

                if (customerCard != null)
                {
                    Console.WriteLine(customerCard.CardNumber);

                    CustomerCardRegistration customerCardRegistration = customerCardRegistrations.Where(x => x.CardId == message.CardId).FirstOrDefault();

                    TimeSpan span = DateTime.Now.Subtract(customerCardRegistration.RegistrationDate);

                    if (span.TotalMinutes <= 30)
                    {
                        if (message.Token == customerCardRegistration.Token)
                            result = true;
                    }
                }
            }

            return result;
        }
    }
}
