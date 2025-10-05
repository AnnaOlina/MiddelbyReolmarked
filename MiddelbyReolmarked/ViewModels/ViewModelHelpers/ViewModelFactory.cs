using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;

namespace MiddelbyReolmarked.ViewModels.ViewModelHelpers
{
    public class ViewModelFactory
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRackRepository _rackRepository;
        private readonly IRentalAgreementRepository _rentalAgreementRepository;

        public ViewModelFactory(
            ICustomerRepository customerRepository, 
            IRackRepository rackRepository, 
            IRentalAgreementRepository rentalAgreementRepository
            )
        {
            _customerRepository = customerRepository;
            _rackRepository = rackRepository;
            _rentalAgreementRepository = rentalAgreementRepository;
        }

        public CustomerViewModel CreateCustomerViewModel(
            Customer customer,
            Action onCustomerChanged = null
        )  // Metodesignatur som tager en Customer som parameter
        {
            return new CustomerViewModel(_customerRepository, customer, onCustomerChanged);
        }

        public RackViewModel CreateRackViewModel(Rack rack)
        {
            return new RackViewModel(_rackRepository, rack);
        }

        public RentalAgreementViewModel CreateRentalAgreementViewModel(
            RentalAgreement rentalAgreement,
            Action onRentalAgreementChanged = null
        )
        {
            return new RentalAgreementViewModel(_rentalAgreementRepository, _customerRepository, rentalAgreement, onRentalAgreementChanged);
        }
    }
}
