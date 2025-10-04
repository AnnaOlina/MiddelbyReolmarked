using System.ComponentModel;
using System.Windows;
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
        private readonly Action _onCustomerChanged;
        private MessageBoxResult _messageBoxResult;

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
        public ICommand DeleteCommand { get; }

        public CustomerViewModel(
            ICustomerRepository customerRepository,
            Customer customer,
            Action onCustomerChanged = null
        )
        {
            _customerRepository = customerRepository;
            _customer = customer;
            _onCustomerChanged = onCustomerChanged;
            SaveCommand = new RelayCommand(_ => Save());
            DeleteCommand = new RelayCommand(_ => Delete());
        }

        private void Save()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                ErrorMessage = "Navn skal udfyldes.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerEmail) || !CustomerEmail.Contains("@") || !CustomerEmail.Contains("."))
            {
                ErrorMessage = "Ugyldig emailadresse.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerPhone) || CustomerPhone.Length < 8)
            {
                ErrorMessage = "Ugyldigt telefonnummer.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_customer.CustomerId == 0)
            {
                _customerRepository.AddCustomer(_customer);
                ErrorMessage = "Kunden er tilføjet.";
            }
            else
            {
                _customerRepository.UpdateCustomer(_customer);
                ErrorMessage = "Kunden er opdateret.";
            }

            MessageBox.Show(ErrorMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            _onCustomerChanged?.Invoke();
        }

        private void Delete()
        {
            ErrorMessage = "";
            if (_customer.CustomerId != 0)
            {
                _messageBoxResult = MessageBox.Show("Er du sikker på, at du vil slette denne kunde?", "Bekræft sletning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (_messageBoxResult == MessageBoxResult.Yes)
                {
                    _customerRepository.DeleteCustomer(_customer.CustomerId);
                    _onCustomerChanged?.Invoke();
                }
            }
            else
            {
                ErrorMessage = "Man kan ikke slette en kunde som endnu ikke er oprettet.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ErrorMessage = "Kunden er nu slettet.";
            MessageBox.Show(ErrorMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            CustomerName = "";
            CustomerPhone = "";
            CustomerEmail = "";
        }
    }
}