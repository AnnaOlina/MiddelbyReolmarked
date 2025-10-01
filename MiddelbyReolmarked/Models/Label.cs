using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddelbyReolmarked.Models
{
    public class Label
    {
        public int LabelId { get; set; }
        public decimal ProductPrice { get; set; }
        public string BarCode { get; set; }
        public DateTime Sold { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RackId { get; set; }
    }
}
