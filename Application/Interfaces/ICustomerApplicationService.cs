using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ICustomerApplicationService
    {
        (bool validation, string errorMessage) Add(CreateCustomerRequest createCustomer);
        bool Delete(int id);
        IEnumerable<CustomerResult> GetAll(Predicate<Customer> predicate = null);
        IEnumerable<CustomerResult> GetAllByCompany(string company);
        IEnumerable<CustomerResult> GetAllByFullname(string fullname);
        CustomerResult GetBy(Func<Customer, bool> predicate);
        CustomerResult GetByCellphone(string cellphone);
        CustomerResult GetByCommercialPhone(string commercialPhone);
        CustomerResult GetByEmail(string email);
        CustomerResult GetById(int id);
        (bool validation, string errorMessage) Update(int id, UpdateCustomerRequest modelUpdated);
    }
}