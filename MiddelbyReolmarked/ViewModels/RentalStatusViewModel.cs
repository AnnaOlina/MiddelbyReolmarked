using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RentalStatusViewModel : BaseViewModel
    {
        private readonly IRentalStatusRepository _repo;

        private ObservableCollection<RentalStatus> _rentalStatuses = new ObservableCollection<RentalStatus>();
        public ObservableCollection<RentalStatus> RentalStatuses
        {
            get { return _rentalStatuses; }
            set
            {
                if (_rentalStatuses != value)
                {
                    _rentalStatuses = value;
                    OnPropertyChanged(nameof(RentalStatuses));
                }
            }
        }

        private RentalStatus _selectedRentalStatus;
        public RentalStatus SelectedRentalStatus
        {
            get { return _selectedRentalStatus; }
            set
            {
                if (_selectedRentalStatus != value)
                {
                    _selectedRentalStatus = value;
                    OnPropertyChanged(nameof(SelectedRentalStatus));
                    ErrorMessage = ""; // Nulstil fejl ved valg
                }
            }
        }

        private string _errorMessage;
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

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public RentalStatusViewModel(IRentalStatusRepository repo)
        {
            _repo = repo;
            AddCommand = new RelayCommand(_ => AddRentalStatus());
            UpdateCommand = new RelayCommand(_ => UpdateRentalStatus());
            DeleteCommand = new RelayCommand(_ => DeleteRentalStatus());

            LoadRentalStatuses();
        }

        private void LoadRentalStatuses()
        {
            RentalStatuses.Clear();
            foreach (var status in _repo.GetAllRentalStatuses())
            {
                RentalStatuses.Add(status);
            }
        }

        private void AddRentalStatus()
        {
            ErrorMessage = "";
            if (SelectedRentalStatus == null || string.IsNullOrWhiteSpace(SelectedRentalStatus.RentalStatusName))
            {
                ErrorMessage = "Navn skal udfyldes.";
                return;
            }

            var newStatus = new RentalStatus
            {
                RentalStatusName = SelectedRentalStatus.RentalStatusName
            };
            _repo.AddRentalStatus(newStatus);
            LoadRentalStatuses();
        }

        private void UpdateRentalStatus()
        {
            ErrorMessage = "";
            if (SelectedRentalStatus == null)
            {
                ErrorMessage = "Vælg en status.";
                return;
            }
            if (string.IsNullOrWhiteSpace(SelectedRentalStatus.RentalStatusName))
            {
                ErrorMessage = "Navn skal udfyldes.";
                return;
            }

            _repo.UpdateRentalStatus(SelectedRentalStatus);
            LoadRentalStatuses();
        }

        private void DeleteRentalStatus()
        {
            ErrorMessage = "";
            if (SelectedRentalStatus == null)
            {
                ErrorMessage = "Vælg en status.";
                return;
            }

            _repo.DeleteRentalStatus(SelectedRentalStatus.RentalStatusId);
            LoadRentalStatuses();
        }
    }
}