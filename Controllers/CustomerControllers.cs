using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Quali8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerControllers : ControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomerControllers(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService ?? throw new ArgumentNullException(nameof(customerApplicationService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SafeAction(() =>
            {
                var customers = _customerApplicationService.GetAll(null);
                return Ok(customers);
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return SafeAction(() =>
            {
                var IdProtection = _customerApplicationService.GetById(id);
                return IdProtection is null
                    ? NotFound($"Client not found! for id: {id}")
                    : Ok(IdProtection);
            });
        }

        [HttpGet("getByFullname/{fullname}")]
        public IActionResult GetByFullname(string fullname)
        {
            return SafeAction(() =>
            {
                var customers = _customerApplicationService.GetAllByFullname(fullname);
                return customers.FirstOrDefault() is null
                    ? NotFound($"Client not found! For FullName: {fullname}")
                    : Ok(customers);
            });
        }

        [HttpGet("getByCompany/{company}")]
        public IActionResult GetByCompany(string company)
        {
            return SafeAction(() =>
            {
                var customers = _customerApplicationService.GetAllByCompany(company);
                return customers.FirstOrDefault() is null
                    ? NotFound($"Client not found! For Company: {company}")
                    : Ok(customers);
            });
        }

        [HttpGet("getByEmail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            return SafeAction(() =>
            {
                var customer = _customerApplicationService.GetByEmail(email);
                return customer is null
                    ? NotFound($"Client not found! For Email: {email}")
                    : Ok(customer);
            });
        }

        [HttpGet("getByCellphone/{cellphone}")]
        public IActionResult GetByCellphone(string cellphone)
        {
            return SafeAction(() =>
            {
                var customer = _customerApplicationService.GetByCellphone(cellphone);
                return customer is null
                    ? NotFound($"Client not found! For Cellphone: {cellphone}")
                    : Ok(customer);
            });
        }

        [HttpGet("getByCommercialPhone/{commercialPhone}")]
        public IActionResult GetByCommercialPhone(string commercialPhone)
        {
            return SafeAction(() =>
            {
                var customer = _customerApplicationService.GetByCommercialPhone(commercialPhone);
                return customer is null
                    ? NotFound($"Client not found! For CommercialPhone: {commercialPhone}")
                    : Ok(customer);
            });
        }

        [HttpPost]
        public IActionResult Post(CreateCustomerRequest createcustomer)
        {
            return SafeAction(() =>
            {
                var id = _customerApplicationService.Add(createcustomer);
                return id.validation
                    ? Created("~api/customer", "ID:" + id)
                    : BadRequest();
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateCustomerRequest customerUp)
        {
            return SafeAction(() =>
            {
                var updatedCustomer = _customerApplicationService.Update(id, customerUp);
                return updatedCustomer.validation
                    ? Ok()
                    : NotFound(updatedCustomer.errorMessage);
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return SafeAction(() =>
            {
                return !_customerApplicationService.Delete(id)
                    ? NotFound()
                    : NoContent();
            });
        }
        private IActionResult SafeAction(Func<IActionResult> action)
        {
            try
            {
                return action?.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}