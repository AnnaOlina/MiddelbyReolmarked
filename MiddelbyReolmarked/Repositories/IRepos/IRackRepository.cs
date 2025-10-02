using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddelbyReolmarked.Models;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IRackRepository
    {
        // Her defineres metoder til CRUD operationer for Rack entiteten i databasen

        // CRUD
        void AddRack(Rack rack);
        Rack GetRackById(int id);
        IEnumerable<Rack> GetAllRacks();
        void UpdateRack(Rack rack);
        void DeleteRack(int id);

        // Ekstra
        List<int> ListAvailableRackIds();
        Rack GetRackByNumber(string rackNumber);
        Customer GetRenterByRackNumber(string rackNumber);
    }
}
