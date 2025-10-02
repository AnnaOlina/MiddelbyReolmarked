using System.ComponentModel;
using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class AddCustomerViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly ICustomerRepository _repo;
        private Customer _newCustomer = new Customer();

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

        public string CustomerName
        {
            get => _newCustomer.CustomerName;
            set
            {
                if (_newCustomer.CustomerName != value)
                {
                    _newCustomer.CustomerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        public string CustomerEmail
        {
            get => _newCustomer.CustomerEmail;
            set
            {
                if (_newCustomer.CustomerEmail != value)
                {
                    _newCustomer.CustomerEmail = value;
                    OnPropertyChanged(nameof(CustomerEmail));
                }
            }
        }

        public string CustomerPhone
        {
            get => _newCustomer.CustomerPhone;
            set
            {
                if (_newCustomer.CustomerPhone != value)
                {
                    _newCustomer.CustomerPhone = value;
                    OnPropertyChanged(nameof(CustomerPhone));
                }
            }
        }

        public ICommand AddCommand { get; }

        public AddCustomerViewModel(ICustomerRepository repo)
        {
            _repo = repo;
            AddCommand = new RelayCommand(_ => AddCustomer());
        }

        private void AddCustomer()
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

            _repo.AddCustomer(_newCustomer);
            ErrorMessage = "Customer added successfully.";
            // Optionally clear fields or notify UI
        }

        // IDataErrorInfo for WPF validation
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(CustomerName))
                {
                    if (string.IsNullOrWhiteSpace(CustomerName))
                        return "CustomerName cannot be empty.";
                }
                if (columnName == nameof(CustomerEmail))
                {
                    if (!string.IsNullOrWhiteSpace(CustomerEmail))
                    {
                        if (!CustomerEmail.Contains("@") || !CustomerEmail.Contains("."))
                            return "Invalid email format.";
                    }
                }
                if (columnName == nameof(CustomerPhone))
                {
                    if (string.IsNullOrWhiteSpace(CustomerPhone) || CustomerPhone.Length < 8)
                        return "Invalid phone number.";
                }
                return null;
            }
        }

        public string Error => null;
    }
}