using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using customercrud.Models;

namespace customercrud.Data.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> ListCustomers();
        Customer GetCustomer(string id);
        void DeleteCustomer(string id);
        void UpdateCustomer(Customer customer);
        void CreateCustomer(Customer customer);
    }
}