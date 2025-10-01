using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Repositories.IRepos
{
    public interface IInvoiceLineRepository
    {
        // Her defineres metoder til CRUD operationer for InvoiceLine entiteten
        void AddInvoiceLine(Models.InvoiceLine invoiceLine);
        Models.InvoiceLine GetInvoiceLineById(int id);
        IEnumerable<Models.InvoiceLine> GetAllInvoiceLines();
        void UpdateInvoiceLine(Models.InvoiceLine invoiceLine);
        void DeleteInvoiceLine(int id);
    }
}
