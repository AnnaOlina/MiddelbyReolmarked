using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class Rack
    {
        public int RackId { get; set; }
        [Required(ErrorMessage = "RackNumber is required.")]
        public string RackNumber { get; set; }
        public int AmountShelves { get; set; }
        public bool HangerBar { get; set; }
    }
}
