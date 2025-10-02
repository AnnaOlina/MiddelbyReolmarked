using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // FIELDS
        private readonly ICustomerRepository _customerRepository;
        private readonly IRackRepository _rackRepository;
        private readonly IRentalAgreementRepository _rentalAgreementRepository;

        private BaseViewModel _currentView;

        // PROPERTIES
        public BaseViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        // COMMANDS
        public ICommand ShowRentalAgreementViewCommand { get; }
        public ICommand ShowAvailableRacksViewCommand { get; }
        public ICommand ShowRackListViewCommand { get; }
        public ICommand ShowCustomerListViewCommand { get; }

        // CONSTRUCTOR
        public MainViewModel(
            ICustomerRepository customerRepository,
            IRackRepository rackRepository,
            IRentalAgreementRepository rentalAgreementRepository)
        {
            _customerRepository = customerRepository;
            _rackRepository = rackRepository;
            _rentalAgreementRepository = rentalAgreementRepository;

            ShowRentalAgreementViewCommand = new RelayCommand(ShowRentalAgreementView);
            ShowAvailableRacksViewCommand = new RelayCommand(ShowAvailableRacksView);
            ShowRackListViewCommand = new RelayCommand(ShowRackListView);
            ShowCustomerListViewCommand = new RelayCommand(ShowCustomerListView);

            // Startside: fx rack-oversigt
            CurrentView = new RackListViewModel(_rackRepository);
        }

        // METHODS

        private void ShowRentalAgreementView()
        {
            CurrentView = new RentalAgreementViewModel(
                _rentalAgreementRepository,
                _rackRepository,
                _customerRepository,
                // Hvis du har RentalStatusRepository, tilføj det her
                null
            );
        }

        private void ShowAvailableRacksView()
        {
            // Her kan du vise en ViewModel med kun ledige reoler
            CurrentView = new RackListViewModel(_rackRepository);
            // Evt. filtrer listen i ViewModel
        }

        private void ShowRackListView()
        {
            CurrentView = new RackListViewModel(_rackRepository);
        }

        private void ShowCustomerListView()
        {
            // Her kan du vise en ViewModel med alle kunder
            CurrentView = new CustomerListViewModel(_customerRepository);
        }
    }
}