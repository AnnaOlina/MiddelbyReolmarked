using System.Collections.Generic;
using MiddelbyReolmarked.Models;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IRentalStatusRepository
    {
        void AddRentalStatus(RentalStatus rentalStatus);
        RentalStatus GetRentalStatusById(int id);
        IEnumerable<RentalStatus> GetAllRentalStatuses();
        void UpdateRentalStatus(RentalStatus rentalStatus);
        void DeleteRentalStatus(int id);
    }
}