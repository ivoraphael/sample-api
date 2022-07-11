using CustomerIntegrationApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CustomerIntegrationApp.Repositories
{
    public interface ICustomerIntegrationRepository
    {
        void SaveCustomerCard(CustomerCard message);
        void SaveRegistration(CustomerCardRegistration message);
        List<CustomerCard> GetCustomerCards();
        List<CustomerCardRegistration> GetRegistrations();
    }

    public class CustomerIntegrationRepository : ICustomerIntegrationRepository
    {
        private readonly string customerCardPath = @$"{Environment.CurrentDirectory}\customerCards.txt";
        private readonly string registrationPath = @$"{Environment.CurrentDirectory}\customerCardRegistrations.txt";

        public CustomerIntegrationRepository()
        {
            if (!File.Exists(customerCardPath))
                File.Create(customerCardPath);

            if (!File.Exists(registrationPath))
                File.Create(registrationPath);
        }

        public void SaveCustomerCard(CustomerCard message)
        {
            TextWriter tw = new StreamWriter(customerCardPath);
            tw.WriteLine(Regex.Replace(JsonConvert.SerializeObject(message).ToString(), @"^\s*$\n", string.Empty, RegexOptions.Multiline).TrimEnd());
            tw.Close();
        }

        public void SaveRegistration(CustomerCardRegistration message)
        {
            TextWriter tw = new StreamWriter(registrationPath);
            tw.WriteLine(Regex.Replace(JsonConvert.SerializeObject(message).ToString(), @"^\s*$\n", string.Empty, RegexOptions.Multiline).TrimEnd());
            tw.Close();
        }

        public List<CustomerCard> GetCustomerCards()
        {
            List<CustomerCard> customerCards = new List<CustomerCard>();

            foreach (string line in File.ReadLines(customerCardPath))
            {
                try
                {
                    customerCards.Add(JsonConvert.DeserializeObject<CustomerCard>(line));
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Could not read line: {ex.Message}");
                }
            }

            return customerCards;
        }

        public List<CustomerCardRegistration> GetRegistrations()
        {
            List<CustomerCardRegistration> customerCardRegistrations = new List<CustomerCardRegistration>();

            foreach (string line in File.ReadLines(registrationPath))
            {
                try
                {
                    customerCardRegistrations.Add(JsonConvert.DeserializeObject<CustomerCardRegistration>(line));
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Could not read line: {ex.Message}");
                }
            }

            return customerCardRegistrations;
        }
    }
}
