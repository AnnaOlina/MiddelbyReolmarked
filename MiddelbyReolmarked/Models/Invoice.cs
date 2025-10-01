using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; } = new();
    }
}
