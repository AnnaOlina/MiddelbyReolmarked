using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class Rack
    {
        public int RackId { get; set; }
        public string RackNumber { get; set; }
        public int AmountShelves { get; set; }
        public bool HangerBar { get; set; }
    }
}
