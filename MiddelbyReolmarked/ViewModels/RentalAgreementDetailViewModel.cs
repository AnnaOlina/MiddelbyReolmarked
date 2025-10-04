using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.IRepos;
using MiddelbyReolmarked.ViewModels.ViewModelHelpers;

namespace MiddelbyReolmarked.ViewModels
{
    public class RentalAgreementDetailViewModel : BaseViewModel
    {
        // FIELDS - REPOSITORIES
        private readonly IRentalAgreementRackRepository _rentalAgreementRackRepo;

        // PROPERTIES - DATA
        public ObservableCollection<Rack> Racks { get; set; }

        // CONSTRUCTOR - INJECT REPOSITORIES
        public RentalAgreementDetailViewModel(IRentalAgreementRackRepository repo, int rentalAgreementId)
        {
            _rentalAgreementRackRepo = repo;
        }

    }
}
