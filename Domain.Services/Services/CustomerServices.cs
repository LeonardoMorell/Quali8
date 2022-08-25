using Domain.Models;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.Services
{
    public class CustomerServices : ICustomerServices
    {
        private List<Customer> CustomersClients = new();

        public List<Customer> GetAll(Predicate<Customer> predicate = null)
        {
            if (predicate is null) return CustomersClients;
            return CustomersClients.FindAll(predicate);
        }

        public Customer GetBy(Func<Customer, bool> predicate)
        {
            var customer = CustomersClients.FirstOrDefault(predicate);
            return customer;
        }

        public (bool validation, string errorMessage) Add(Customer customer)
        {
            var method = CheckIfExists(customer);
            if (method.exists)
            {
                return (false, method.errorMessage);
            }
            int LastId = 0;
            if (CustomersClients.Count == 0)
            {
                customer.Id = LastId + 1;
                CustomersClients.Add(customer);
            }
            else
            {
                LastId = CustomersClients.Last().Id;
                customer.Id = LastId + 1;
                CustomersClients.Add(customer);
            }
            return (true, customer.Id.ToString());
        }

        public bool Delete(int id)
        {
            var customer = GetBy(c => c.Id == id);
            if (customer == null) return false;
            CustomersClients.Remove(customer);
            return true;
        }

        public (bool validation, string errorMessage) Update(int id, Customer CustomerUpdated)
        {
            var customerVerify = CheckIfExists(CustomerUpdated);
            var customerGetBy = GetBy(x => x.Id == id);
            if (customerVerify.exists)
            {
                return (false, customerVerify.errorMessage);
            }
            var indexCustomer = CustomersClients.IndexOf(CustomersClients.FirstOrDefault(customerGetBy));
            if (indexCustomer is -1)
            {
                return (false, $"Not found id: {id}");
            }
            CustomersClients[indexCustomer] = CustomerUpdated;
            return (true, customerGetBy.Id.ToString());
        }

        private (bool exists, string errorMessage) CheckIfExists(Customer customer)
        {
            var messageTemplate = "Customer exists for {0}: {1}, {2}";
            if (CustomersClients.Any(x => x.Email.Equals(customer.Email)))
            {
                return (true, string.Format("{0}",messageTemplate, "Email", customer.Email));
            }
            if (CustomersClients.Any(x => x.Cellphone.Equals(customer.Cellphone)))
            {
                return (true, string.Format("{1}", messageTemplate, "Cellphone", customer.Cellphone));
            }
            if (CustomersClients.Any(x => x.CommercialPhone.Equals(customer.CommercialPhone)))
            {
                return (true, string.Format("{2}", messageTemplate, "CommercialPhone", customer.CommercialPhone));
            }
            return default;
        }
    }
}