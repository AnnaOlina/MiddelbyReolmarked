using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Utils;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels;

public class MainViewModel : BaseViewModel
{
    // FIELDS
    private readonly RentalAgreementListViewModel _rentalAgreementViewModel;
    private readonly RackListViewModel _rackListViewModel;
    private readonly RackListViewModel _availableRackListViewModel;
    private readonly CustomerListViewModel _customerListViewModel;
    private readonly ViewModelFactory _viewModelFactory;

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
    public ICommand ShowCustomerViewCommand { get; }
    

    // CONSTRUCTOR
    public MainViewModel(
        RentalAgreementListViewModel rentalAgreementViewModel,
        RackListViewModel rackListViewModel,
        RackListViewModel availableRackListViewModel,
        CustomerListViewModel customerListViewModel,
        ViewModelFactory viewModelFactory,
        CurrentViewService currentViewService
    )
    {
        _rentalAgreementViewModel = rentalAgreementViewModel;
        _rackListViewModel = rackListViewModel;
        _availableRackListViewModel = availableRackListViewModel;
        _customerListViewModel = customerListViewModel;
        _viewModelFactory = viewModelFactory;

        currentViewService.OnViewChanged = (newView) =>
        {
            CurrentView = newView;
        };

        ShowRentalAgreementViewCommand = new RelayCommand(_ => ShowRentalAgreementView());
        ShowAvailableRacksViewCommand = new RelayCommand(_ => ShowAvailableRacksView());
        ShowRackListViewCommand = new RelayCommand(_ => ShowRackListView());
        ShowCustomerListViewCommand = new RelayCommand(_ => ShowCustomerListView());
        ShowCustomerViewCommand = new RelayCommand(_ => ShowCustomerView());
       
        //Startside: fx rack-oversigt
        _currentView = rackListViewModel;
    }

    // METHODS

    public void ShowRentalAgreementView()
    {
        CurrentView = _rentalAgreementViewModel;
    }

    public void ShowAvailableRacksView()
    {
        CurrentView = _availableRackListViewModel;
    }

    public void ShowRackListView()
    {
        CurrentView = _rackListViewModel;
    }

    public void ShowCustomerListView()
    {
        CurrentView = _customerListViewModel;
    }

    public void ShowCustomerView()
    {
        CurrentView = _viewModelFactory.CreateCustomerViewModel(new Customer(), _customerListViewModel.RefreshCustomerList);
    }

}