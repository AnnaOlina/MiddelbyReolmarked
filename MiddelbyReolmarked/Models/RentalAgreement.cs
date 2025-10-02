using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class RentalAgreement
    {
        // Properties
        public int RentalAgreementId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // <-- nullable her!
        public int CustomerId { get; set; }
        public int RackId { get; set; }
        public int RentalStatusId { get; set; }
    }
}
