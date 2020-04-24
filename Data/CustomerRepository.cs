using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using customercrud.Models;
using customercrud.Data.Interfaces;

namespace customercrud.Data
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IMongoDatabase MongoDatabase;

        public CustomerRepository(IMongoDatabase mongoDatabase)
        {
            this.MongoDatabase = mongoDatabase;
        }

        public List<Customer> ListCustomers()
        { 
            return this.MongoDatabase.GetCollection<Customer>("Customers").Find(_ => true).ToList();
        }

        public Customer GetCustomer(string id)
        { 
            return 
                this.MongoDatabase.GetCollection<Customer>("Customers")
                    .Find(Builders<Customer>
                    .Filter.Eq("Id", id))
                    .ToList()
                    .FirstOrDefault();
        }

        public void UpdateCustomer(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, customer.Id);
            var update = Builders<Customer>.Update
                            .Set(s => s.Firstname, customer.Firstname)
                            .Set(s => s.Lastname, customer.Lastname);

            this.MongoDatabase.GetCollection<Customer>("Customers").UpdateOne(filter, update);
        }

        public void CreateCustomer(Customer customer)
        {
            this.MongoDatabase.GetCollection<Customer>("Customers").InsertOne(customer);
        }

        public void DeleteCustomer(string id)
        {
           this.MongoDatabase.GetCollection<Customer>("Customers").DeleteOne(Builders<Customer>.Filter.Eq("Id", id));
        }
    }
}