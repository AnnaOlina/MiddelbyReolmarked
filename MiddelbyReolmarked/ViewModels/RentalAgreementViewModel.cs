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
        private readonly IRentalAgreementRackRepository _rentalAgreementRackRepository;

        private ObservableCollection<RentalAgreement> _rentalAgreements;
        private ObservableCollection<Rack> _selectedRacks;
        private RentalAgreement _selectedRentalAgreement;
        private string _errorMessage;

        public RentalAgreementViewModel(
            IRentalAgreementRepository rentalAgreementRepository,
            IRackRepository rackRepository,
            ICustomerRepository customerRepository,
            IRentalAgreementRackRepository rentalAgreementRackRepository)
        {
            _rentalAgreementRepository = rentalAgreementRepository;
            _rackRepository = rackRepository;
            _customerRepository = customerRepository;
            _rentalAgreementRackRepository = rentalAgreementRackRepository;

            _rentalAgreements = new ObservableCollection<RentalAgreement>();
            _selectedRacks = new ObservableCollection<Rack>();
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

        public ObservableCollection<Rack> SelectedRacks
        {
            get { return _selectedRacks; }
            set
            {
                if (_selectedRacks != value)
                {
                    _selectedRacks = value;
                    OnPropertyChanged(nameof(SelectedRacks));
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

        // Hent reoler til en given lejeaftale
        public ObservableCollection<Rack> LoadRacksForAgreement(int rentalAgreementId)
        {
            var racks = new ObservableCollection<Rack>();
            var links = _rentalAgreementRackRepository.GetByRentalAgreementId(rentalAgreementId);
            foreach (var rar in links)
            {
                var rack = _rackRepository.GetRackById(rar.RackId);
                if (rack != null)
                {
                    racks.Add(rack);
                }
            }
            return racks;
        }

        // Opret ny lejeaftale og tilknyt reoler via linking-tabel
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
            if (newAgreement.StartDate == DateTime.MinValue)
            {
                ErrorMessage = "Vælg startdato.";
                return;
            }
            if (SelectedRacks == null || SelectedRacks.Count == 0)
            {
                ErrorMessage = "Vælg mindst én reol.";
                return;
            }

            // Opret lejeaftale
            _rentalAgreementRepository.AddRentalAgreement(newAgreement);

            // Find lejeaftalens id (fx ved at hente det sidste oprettede, eller ændre AddRentalAgreement til at returnere id)
            var agreementId = newAgreement.RentalAgreementId; // Hvis id genereres i AddRentalAgreement

            // Tilknyt racks via linking-tabel
            foreach (var rack in SelectedRacks)
            {
                _rentalAgreementRackRepository.AddRentalAgreementRack(new RentalAgreementRack
                {
                    RentalAgreementId = agreementId,
                    RackId = rack.RackId
                });
            }

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
    }
}