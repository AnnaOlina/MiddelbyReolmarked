using System.Collections.ObjectModel;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }

        public Action<Customer> OnCustomerSelected { get; set; } = (_) => { };

        public CustomerListViewModel(
            ICustomerRepository customerRepository
        )
        {
            _customerRepository = customerRepository;
            _customers = new ObservableCollection<Customer>(_customerRepository.GetAllCustomers());
        }

        public void SelectCustomer(Customer customer)
        {
            OnCustomerSelected(customer);
            //var customerViewModel = customerViewModelFactory.Create(customer);
            //mainViewModel.CurrentView = customerViewModel;
        }

        public void RefreshCustomerList()
        {
            Customers = new ObservableCollection<Customer>(_customerRepository.GetAllCustomers());
        }
    }
}