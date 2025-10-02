using System.Collections.ObjectModel;
using MiddelbyReolmarked.Models;
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
        private Rack _rack;
        private RackTypeOption _selectedRackType;

        public RackViewModel(Rack rack)
        {
            if (rack == null)
            {
                _rack = new Rack();
            }
            else
            {
                _rack = rack;
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

        // Simpel validering
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(RackNumber))
                {
                    if (string.IsNullOrWhiteSpace(RackNumber))
                    {
                        return "RackNumber skal udfyldes.";
                    }
                }
                if (columnName == nameof(AmountShelves))
                {
                    if (AmountShelves != 3 && AmountShelves != 6)
                    {
                        return "AmountShelves skal være 3 eller 6.";
                    }
                }
                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}