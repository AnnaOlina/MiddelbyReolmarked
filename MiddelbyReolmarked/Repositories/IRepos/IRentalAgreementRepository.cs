using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IRentalAgreementRepository
    {
        // Her defineres metoder til CRUD operationer for RentalAgreement entiteten
        void AddRentalAgreement(RentalAgreement rentalAgreement);
        RentalAgreement GetRentalAgreementById(int id);
        IEnumerable<RentalAgreement> GetAllRentalAgreements();
        void UpdateRentalAgreement(RentalAgreement rentalAgreement);
        void DeleteRentalAgreement(int id);
    }
}
