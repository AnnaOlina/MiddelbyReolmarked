using System.ComponentModel;
using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private Customer _customer;

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public int CustomerId => _customer.CustomerId;

        public string CustomerName
        {
            get => _customer.CustomerName;
            set
            {
                if (_customer.CustomerName != value)
                {
                    _customer.CustomerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        public string CustomerEmail
        {
            get => _customer.CustomerEmail;
            set
            {
                if (_customer.CustomerEmail != value)
                {
                    _customer.CustomerEmail = value;
                    OnPropertyChanged(nameof(CustomerEmail));
                }
            }
        }

        public string CustomerPhone
        {
            get => _customer.CustomerPhone;
            set
            {
                if (_customer.CustomerPhone != value)
                {
                    _customer.CustomerPhone = value;
                    OnPropertyChanged(nameof(CustomerPhone));
                }
            }
        }

        public ICommand SaveCommand { get; }

        public CustomerViewModel(
            ICustomerRepository customerRepository,
            Customer customer
        )
        {
            _customerRepository = customerRepository;
            _customer = customer;
            SaveCommand = new RelayCommand(_ => Save());
        }

        private void Save()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                ErrorMessage = "CustomerName cannot be empty.";
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerEmail) || !CustomerEmail.Contains("@") || !CustomerEmail.Contains("."))
            {
                ErrorMessage = "Invalid email format.";
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerPhone) || CustomerPhone.Length < 8)
            {
                ErrorMessage = "Invalid phone number.";
                return;
            }

            if (_customer.CustomerId == 0)
            {
                _customerRepository.AddCustomer(_customer);
            }
            else
            {
                _customerRepository.UpdateCustomer(_customer);
            }

            ErrorMessage = "Customer added successfully.";
        }
    }
}