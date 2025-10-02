using System;
using System.Collections.ObjectModel;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RentalAgreementViewModel : BaseViewModel
    {
        private readonly IRentalAgreementRepository _rentalAgreementRepository;
        private readonly IRackRepository _rackRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalStatusRepository _rentalStatusRepository;

        private ObservableCollection<RentalAgreement> _rentalAgreements;
        private RentalAgreement _selectedRentalAgreement;
        private string _errorMessage;

        public RentalAgreementViewModel(
            IRentalAgreementRepository rentalAgreementRepository,
            IRackRepository rackRepository,
            ICustomerRepository customerRepository,
            IRentalStatusRepository rentalStatusRepository)
        {
            _rentalAgreementRepository = rentalAgreementRepository;
            _rackRepository = rackRepository;
            _customerRepository = customerRepository;
            _rentalStatusRepository = rentalStatusRepository;

            _rentalAgreements = new ObservableCollection<RentalAgreement>();
            LoadRentalAgreements();
        }

        public ObservableCollection<RentalAgreement> RentalAgreements
        {
            get { return _rentalAgreements; }
            set
            {
                if (_rentalAgreements != value)
                {
                    _rentalAgreements = value;
                    OnPropertyChanged(nameof(RentalAgreements));
                }
            }
        }

        public RentalAgreement SelectedRentalAgreement
        {
            get { return _selectedRentalAgreement; }
            set
            {
                if (_selectedRentalAgreement != value)
                {
                    _selectedRentalAgreement = value;
                    OnPropertyChanged(nameof(SelectedRentalAgreement));
                    ErrorMessage = "";
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

        // Hent alle lejeaftaler
        public void LoadRentalAgreements()
        {
            RentalAgreements.Clear();
            var agreementsFromRepo = _rentalAgreementRepository.GetAllRentalAgreements();
            foreach (var agreement in agreementsFromRepo)
            {
                RentalAgreements.Add(agreement);
            }
        }

        // Opret ny lejeaftale
        public void AddRentalAgreement(RentalAgreement newAgreement)
        {
            ErrorMessage = "";

            if (newAgreement == null)
            {
                ErrorMessage = "Lejeaftale mangler.";
                return;
            }
            if (newAgreement.CustomerId <= 0)
            {
                ErrorMessage = "Vælg en kunde.";
                return;
            }
            if (newAgreement.RackId <= 0)
            {
                ErrorMessage = "Vælg en reol.";
                return;
            }
            if (newAgreement.StartDate == DateTime.MinValue)
            {
                ErrorMessage = "Vælg startdato.";
                return;
            }
            if (newAgreement.RentalStatusId <= 0)
            {
                ErrorMessage = "Vælg status.";
                return;
            }

            // Tjek om reolen er ledig
            var availableIds = _rackRepository.ListAvailableRackIds();
            if (!availableIds.Contains(newAgreement.RackId))
            {
                ErrorMessage = "Reolen er ikke ledig.";
                return;
            }

            _rentalAgreementRepository.AddRentalAgreement(newAgreement);
            LoadRentalAgreements();
        }

        // Afslut lejeaftale (sæt slutdato)
        public void EndRentalAgreement(RentalAgreement agreement, DateTime endDate)
        {
            ErrorMessage = "";

            if (agreement == null)
            {
                ErrorMessage = "Vælg en lejeaftale.";
                return;
            }
            if (endDate <= agreement.StartDate)
            {
                ErrorMessage = "Slutdato skal være efter startdato.";
                return;
            }
            agreement.EndDate = endDate;
            _rentalAgreementRepository.UpdateRentalAgreement(agreement);
            LoadRentalAgreements();
        }

        // Simpel validering for binding
        public string this[string columnName]
        {
            get
            {
                if (SelectedRentalAgreement == null)
                {
                    return null;
                }
                if (columnName == nameof(SelectedRentalAgreement.CustomerId))
                {
                    if (SelectedRentalAgreement.CustomerId <= 0)
                    {
                        return "Vælg en kunde.";
                    }
                }
                if (columnName == nameof(SelectedRentalAgreement.RackId))
                {
                    if (SelectedRentalAgreement.RackId <= 0)
                    {
                        return "Vælg en reol.";
                    }
                }
                if (columnName == nameof(SelectedRentalAgreement.StartDate))
                {
                    if (SelectedRentalAgreement.StartDate == DateTime.MinValue)
                    {
                        return "Vælg startdato.";
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