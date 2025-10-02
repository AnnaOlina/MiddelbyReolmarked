using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        //public decimal LineTotal => UnitPrice * Quantity;
        public int InvoiceId { get; set; }
        public int RentalAgreementId { get; set; }
    }
}
