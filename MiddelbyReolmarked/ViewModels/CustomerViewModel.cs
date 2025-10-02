using System.ComponentModel;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class CustomerViewModel : BaseViewModel, IDataErrorInfo
    {
        private Customer _customer;

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
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

        // IDataErrorInfo implementation for validation
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