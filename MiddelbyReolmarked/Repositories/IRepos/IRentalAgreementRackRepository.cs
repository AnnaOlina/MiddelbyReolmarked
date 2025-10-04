using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IRentalAgreementRackRepository
    {
        // Her defineres relevante metoder til CRUD operationer for RentalAgreementRack entiteten i databasen
        void AddRentalAgreementRack(RentalAgreementRack rar);
        IEnumerable<RentalAgreementRack> GetByRentalAgreementId(int rentalAgreementId);
        IEnumerable<RentalAgreementRack> GetByRackId(int rackId);
        void DeleteRentalAgreementRack(int rentalAgreementId, int rackId);
    }
}
