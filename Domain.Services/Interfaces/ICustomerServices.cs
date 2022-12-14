using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ICustomerServices
    {
        public (bool validation, string errorMessage) Add(Customer customer);
        List<Customer> GetAll(Predicate<Customer> predicate = null);
        Customer GetBy(Func<Customer, bool> predicate);
        public (bool validation, string errorMessage) Update(int id, Customer CustomerUpdated);
        bool Delete(int id);
    }
}