using System.Collections.ObjectModel;
using System.Linq;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RackListViewModel : BaseViewModel
    {
        private readonly IRackRepository _rackRepository;

        private ObservableCollection<RackViewModel> _racks;
        private RackViewModel _selectedRack;
        private string _searchRackNumber;
        private string _errorMessage;

        public RackListViewModel(IRackRepository rackRepository)
        {
            _rackRepository = rackRepository;
            _racks = new ObservableCollection<RackViewModel>();
            LoadRacks();
        }

        public ObservableCollection<RackViewModel> Racks
        {
            get { return _racks; }
            set
            {
                if (_racks != value)
                {
                    _racks = value;
                    OnPropertyChanged(nameof(Racks));
                }
            }
        }

        public RackViewModel SelectedRack
        {
            get { return _selectedRack; }
            set
            {
                if (_selectedRack != value)
                {
                    _selectedRack = value;
                    OnPropertyChanged(nameof(SelectedRack));
                    ErrorMessage = ""; // Nulstil fejl
                }
            }
        }

        public string SearchRackNumber
        {
            get { return _searchRackNumber; }
            set
            {
                if (_searchRackNumber != value)
                {
                    _searchRackNumber = value;
                    OnPropertyChanged(nameof(SearchRackNumber));
                }
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        // Hent alle reoler fra repository
        public void LoadRacks()
        {
            Racks.Clear();
            var racksFromRepo = _rackRepository.GetAllRacks();
            foreach (var rack in racksFromRepo)
            {
                Racks.Add(new RackViewModel(rack));
            }
        }

        // Søg efter reolnummer
        public void SearchRack()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(SearchRackNumber))
            {
                ErrorMessage = "Indtast et reolnummer for at søge.";
                return;
            }

            var foundRack = Racks.FirstOrDefault(r => r.RackNumber == SearchRackNumber);
            if (foundRack == null)
            {
                ErrorMessage = "Reolen blev ikke fundet.";
                SelectedRack = null;
                return;
            }

            SelectedRack = foundRack;
        }

        // Filtrer ledige reoler
        public ObservableCollection<RackViewModel> GetAvailableRacks()
        {
            var availableIds = _rackRepository.ListAvailableRackIds();
            var availableRacks = new ObservableCollection<RackViewModel>();

            foreach (var rack in Racks)
            {
                if (availableIds.Contains(rack.RackId))
                {
                    availableRacks.Add(rack);
                }
            }
            return availableRacks;
        }
    }
}