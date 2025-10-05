using System.Collections.ObjectModel;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.Utils;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ViewModelFactory _viewModelFactory;
        private readonly CurrentViewService _currentViewService;
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
        
        public CustomerListViewModel(
            ICustomerRepository customerRepository,
            ViewModelFactory viewModelFactory,
            CurrentViewService currentViewService
        )
        {
            _customerRepository = customerRepository;
            _viewModelFactory = viewModelFactory;
            _currentViewService = currentViewService;
            _customers = new ObservableCollection<Customer>(_customerRepository.GetAllCustomers());
        }

        public void SelectCustomer(Customer customer)
        {
            var customerViewModel = _viewModelFactory.CreateCustomerViewModel(customer, RefreshCustomerList);
            _currentViewService.OnViewChanged?.Invoke(customerViewModel);
        }

        public void RefreshCustomerList()
        {
            Customers = new ObservableCollection<Customer>(_customerRepository.GetAllCustomers());
        }
    }
}