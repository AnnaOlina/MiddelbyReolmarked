using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IInvoiceRepository
    {
        // Her defineres metoder til CRUD operationer for Invoice entiteten i databasen
        void AddInvoice(Models.Invoice invoice);
        Models.Invoice GetInvoiceById(int id);
        IEnumerable<Models.Invoice> GetAllInvoices();
        void UpdateInvoice(Models.Invoice invoice);
        void DeleteInvoice(int id);
    }
}
