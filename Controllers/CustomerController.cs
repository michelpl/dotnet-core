using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using customercrud.Models;
using customercrud.Data;
using MongoDB.Driver;

namespace customercrud.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("v1/customers")]
    public class CustomerController : Controller
    {
        private Context context;

        public CustomerController()
        {
            context = new Context();
        }

        // GET: api/customer
        [HttpGet]
        [Route("")]
        public IEnumerable<Customer> Get()
        {
            var customers = context.Customers.Find(_ => true).ToList();
            return customers;
        }

        // POST: api/customer
        [HttpPost]
        [Route("")]
        public Customer Customer([FromBody]Customer customer)
        {
            context.Customers.InsertOne(customer);
            return customer;
        }
    }
}