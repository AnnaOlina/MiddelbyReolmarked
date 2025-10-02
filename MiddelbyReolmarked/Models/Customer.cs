using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "CustomerName må ikke være tom")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "CustomerPhone må ikke være tom")]
        [StringLength(100)]
        public string CustomerPhone { get; set; }


        [Required(ErrorMessage = "CustomerEmail må ikke være tom")]
        [StringLength(100)]
        public string CustomerEmail { get; set; }
    }

    /* Validering hører hjemme i Model-klassen og ikke i Repository, fordi:
     * Model-klassen repræsenterer domæne-data og regler: 
     * Her defineres, hvad en gyldig Customer er.
     */
}
