using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using customercrud.Models;
using customercrud.Data.Interfaces;
using MongoDB.Driver;

namespace customercrud.Controllers
{
    [Produces("application/json")]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository CustomerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult ListCustomers()
        {
            var customers = this.CustomerRepository.ListCustomers();
            return Ok(customers);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateCustomer([FromBody]Customer customer)
        {
            this.CustomerRepository.CreateCustomer(customer);
            return Ok();
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        public IActionResult GetCustomer(string id)
        {
            var customer = this.CustomerRepository.GetCustomer(id);

            if (customer == null) 
            {
                return NotFound();
            }

            return Ok(customer);
            
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateCustomer(string id, [FromBody]Customer customer)
        {
            var oldCustomer = this.CustomerRepository.GetCustomer(id);

            if (oldCustomer == null) 
            {
                return NotFound();
            }

            customer.Id = id;
            this.CustomerRepository.UpdateCustomer(customer);

            return Ok(customer);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCustomer(string id)
        {
            var customer = this.CustomerRepository.GetCustomer(id);

            if (customer == null) 
            {
                return NotFound();
            }

            this.CustomerRepository.DeleteCustomer(id);

            return Ok();
        }
    }
}