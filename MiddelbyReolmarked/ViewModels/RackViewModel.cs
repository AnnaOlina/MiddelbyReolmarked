using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.DbRepos;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RackTypeOption
    {
        public string DisplayName { get; set; }
        public int AmountShelves { get; set; }
        public bool HangerBar { get; set; }
    }

    public class RackViewModel : BaseViewModel
    {
        private readonly IRackRepository _rackRepository;
        private Rack _rack;
        private RackTypeOption _selectedRackType;
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

        public int RackId
        {
            get { return _rack.RackId; }
            set
            {
                if (_rack.RackId != value)
                {
                    _rack.RackId = value;
                    OnPropertyChanged(nameof(RackId));
                }
            }
        }

        public string RackNumber
        {
            get { return _rack.RackNumber; }
            set
            {
                if (_rack.RackNumber != value)
                {
                    _rack.RackNumber = value;
                    OnPropertyChanged(nameof(RackNumber));
                }
            }
        }

        public int AmountShelves
        {
            get { return _rack.AmountShelves; }
            set
            {
                if (_rack.AmountShelves != value)
                {
                    _rack.AmountShelves = value;
                    OnPropertyChanged(nameof(AmountShelves));
                }
            }
        }

        public bool HangerBar
        {
            get { return _rack.HangerBar; }
            set
            {
                if (_rack.HangerBar != value)
                {
                    _rack.HangerBar = value;
                    OnPropertyChanged(nameof(HangerBar));
                }
            }
        }

        public RackTypeOption SelectedRackType
        {
            get { return _selectedRackType; }
            set
            {
                if (_selectedRackType != value)
                {
                    _selectedRackType = value;
                    if (_selectedRackType != null)
                    {
                        AmountShelves = _selectedRackType.AmountShelves;
                        HangerBar = _selectedRackType.HangerBar;
                    }
                    OnPropertyChanged(nameof(SelectedRackType));
                }
            }
        }

        public ObservableCollection<RackTypeOption> RackTypeOptions { get; } = new ObservableCollection<RackTypeOption>
        {
            new RackTypeOption { DisplayName = "6 hylder", AmountShelves = 6, HangerBar = false },
            new RackTypeOption { DisplayName = "3 hylder + bøjlestang", AmountShelves = 3, HangerBar = true }
        };

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public RackViewModel(IRackRepository rackRepository, Rack rack)
        {
            _rackRepository = rackRepository;
            _rack = rack;
            SaveCommand = new RelayCommand(_ => Save());
            //DeleteCommand = new RelayCommand(_ => Delete());
        }

        public void Save()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(RackNumber))
            {
                ErrorMessage = "Reolnummer skal udfyldes.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_selectedRackType == null)
            {
                ErrorMessage = "Reoltype skal vælges.";
                MessageBox.Show(ErrorMessage, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _rackRepository.UpdateRack(_rack);
            ErrorMessage = "Reol er opdateret.";
            MessageBox.Show(ErrorMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}