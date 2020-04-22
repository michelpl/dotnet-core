using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using customercrud.Models;

namespace customercrud.Data
{
    public class Context
    {
        private readonly IMongoDatabase database;

        public Context()
        {
            database = new MongoClient("mongodb://localhost:27017").GetDatabase("customerCrud");
        }

        public IMongoCollection<Customer> Customers
        { 
            get
            {
                return database.GetCollection<Customer>("Customers");
            }
        }

                public IMongoCollection<Post> Posts
        { 
            get
            {
                return database.GetCollection<Post>("Posts");
            }
        }
    }
}