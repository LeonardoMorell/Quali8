using Application.Interfaces;
using System.Collections.Generic;
using System;
using Domain.Models;
using Domain.Services.Interfaces;
using AutoMapper;
using Application.Models.Responses;
using Application.Models.Requests;

namespace Application.Services
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerServices _customerServices;

        private readonly IMapper _mapper;

        public CustomerApplicationService(ICustomerServices customerServices, IMapper mapper)
        {
            _customerServices = customerServices ?? throw new ArgumentNullException(nameof(customerServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<CustomerResult> GetAll(Predicate<Customer> predicate = null)
        {
            var customer = _customerServices.GetAll();
            return _mapper.Map<IEnumerable<CustomerResult>>(customer);
        }

        public CustomerResult GetBy(Func<Customer, bool> predicate)
        {
            var customer = _customerServices.GetBy(predicate);
            return _mapper.Map<CustomerResult>(customer);
        }

        public (bool validation, string errorMessage) Add(CreateCustomerRequest createCustomer)
        {
            var customerCreate = _mapper.Map<Customer>(createCustomer);
            var customerAdd = _customerServices.Add(customerCreate);
            if (customerAdd.validation) return (true, customerAdd.errorMessage);

            return (false, customerAdd.errorMessage);
        }

        public bool Delete(int id)
        {
            return _customerServices.Delete(id);
        }

        public (bool validation, string errorMessage) Update(int id, UpdateCustomerRequest modelUpdated)
        {
            var customer = _mapper.Map<Customer>(modelUpdated);
            return _customerServices.Update(id, customer);
        }

        public CustomerResult GetById(int id)
        {
            var customer = _customerServices.GetBy(x => x.Id == id);
            return _mapper.Map<CustomerResult>(customer);
        }

        public IEnumerable<CustomerResult> GetAllByFullname(string fullname)
        {
            var customer = _customerServices.GetAll(x => x.Fullname == fullname);
            return _mapper.Map<IEnumerable<CustomerResult>>(customer);
        }

        public IEnumerable<CustomerResult> GetAllByCompany(string company)
        {
            var customer = _customerServices.GetAll(x => x.Company == company);
            return _mapper.Map<IEnumerable<CustomerResult>>(customer);
        }

        public CustomerResult GetByEmail(string email)
        {
            var customer = _customerServices.GetBy(x => x.Email == email);
            return _mapper.Map<CustomerResult>(customer);
        }

        public CustomerResult GetByCellphone(string cellphone)
        {
            var customer = _customerServices.GetBy(x => x.Cellphone == cellphone);
            return _mapper.Map<CustomerResult>(customer);
        }

        public CustomerResult GetByCommercialPhone(string commercialPhone)
        {
            var customer = _customerServices.GetBy(x => x.CommercialPhone == commercialPhone);
            return _mapper.Map<CustomerResult>(customer);
        }
    }
}