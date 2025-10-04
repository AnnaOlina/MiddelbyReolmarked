using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.ViewModels.ViewModelHelpers
{
    public class CustomerViewModelFactory
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerViewModelFactory(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerViewModel Create(
            Customer customer,
            Action onCustomerChanged = null
        )  // Metodesignatur som tager en Customer som parameter
        {
            return new CustomerViewModel(_customerRepository, customer, onCustomerChanged);
        }
    }
}
