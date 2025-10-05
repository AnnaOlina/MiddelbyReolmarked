using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RentalAgreementViewModel : BaseViewModel
    {
        private readonly IRentalAgreementRepository _rentalAgreementRepository;
        private readonly ICustomerRepository _customerRepository;
        private RentalAgreement _rentalAgreement;
        private readonly Action _onRentalAgreementChanged;
        private MessageBoxResult _messageBoxResult;

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    _rentalAgreement.CustomerId = _selectedCustomer.CustomerId;
                    OnPropertyChanged(nameof(SelectedCustomer));
                }
            }
        }

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

        public int RentalAgreementId => _rentalAgreement.RentalAgreementId;

        public DateTime StartDate
        {
            get => _rentalAgreement.StartDate;
            set
            {
                if (_rentalAgreement.StartDate != value)
                {
                    _rentalAgreement.StartDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime? EndDate
        {
            get => _rentalAgreement.EndDate;
            set
            {
                if (_rentalAgreement.EndDate != value)
                {
                    _rentalAgreement.EndDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public int CustomerId
        {
            get => _rentalAgreement.CustomerId;
            set
            {
                if (_rentalAgreement.CustomerId != value)
                {
                    _rentalAgreement.CustomerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                }
            }
        }

        public decimal MonthlyPrice
        {
            get => 850;
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public RentalAgreementViewModel(
            IRentalAgreementRepository rentalAgreementRepository, 
            ICustomerRepository customerRepository, 
            RentalAgreement rentalAgreement, 
            Action onRentalAgreementChanged
        )
        {
            _rentalAgreementRepository = rentalAgreementRepository;
            _customerRepository = customerRepository;
            _rentalAgreement = rentalAgreement ?? new RentalAgreement { StartDate = DateTime.Today };
            _onRentalAgreementChanged = onRentalAgreementChanged;
            SaveCommand = new RelayCommand(_ => Save());
            //DeleteCommand = new RelayCommand(Delete);

            Customers = new ObservableCollection<Customer>(_customerRepository.GetAllCustomers());
        }

        private void Save()
        {
            ErrorMessage = "";
            if (_rentalAgreement == null)
            {
                ErrorMessage = "Lejeaftale mangler.";
                return;
            }
            if (_rentalAgreement.CustomerId <= 0)
            {
                ErrorMessage = "Vælg en kunde.";
                return;
            }
            if (_rentalAgreement.StartDate == DateTime.MinValue)
            {
                ErrorMessage = "Vælg startdato.";
                return;
            }
            if (_rentalAgreement.EndDate.HasValue && _rentalAgreement.EndDate < _rentalAgreement.StartDate)
            {
                ErrorMessage = "Slutdato kan ikke være før startdato.";
                return;
            }

            if (_rentalAgreement.RentalAgreementId == 0)
            {
                _rentalAgreementRepository.AddRentalAgreement(_rentalAgreement);
            }
            else
            {
                _rentalAgreementRepository.UpdateRentalAgreement(_rentalAgreement);
            }
            _onRentalAgreementChanged?.Invoke();
        }


    }
}
