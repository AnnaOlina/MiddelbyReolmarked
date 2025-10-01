using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface ICustomerRepository
    {
        // Her defineres metoder til CRUD operationer for Customer entiteten
        void AddCustomer(Customer customer);
        Customer GetCustomerById(int id);
        IEnumerable<Customer> GetAllCustomers();
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
