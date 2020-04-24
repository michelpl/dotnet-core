using System.Security.Cryptography.X509Certificates;
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

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<Customer> Get()
        {
            var customers = context.Customers.Find(_ => true).ToList();
            return customers;
        }

        [HttpPost]
        [Route("")]
        public Customer Post([FromBody]Customer customer)
        {
            context.Customers.InsertOne(customer);
            return customer;
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        public IEnumerable<Customer> GetById(string id)
        {

            var customer = context.Customers.Find(Builders<Customer>.Filter.Eq("Id", id)).ToList();
            //context.Customers.Find(customer => customer.Id == id).ToList();
            return customer;
        }

        [HttpPut("{id:length(24)}")]
        public void Put(string id, [FromBody]Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, id);
            var update = Builders<Customer>.Update
                            .Set(s => s.Firstname, customer.Firstname)
                            .Set(s => s.Lastname, customer.Lastname);

            context.Customers.UpdateOne(filter, update);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public void Delete(string id)
        {
            context.Customers.DeleteOneAsync(Builders<Customer>.Filter.Eq("Id", id));
        }
    }
}