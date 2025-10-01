using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface ILabelRepository
    {
        // Her defineres metoder til CRUD operationer for Label entiteten
        void AddLabel(Models.Label label);
        Models.Label GetLabelById(int id);
        IEnumerable<Models.Label> GetAllLabels();
        void UpdateLabel(Models.Label label);
        void DeleteLabel(int id);
    }
}
