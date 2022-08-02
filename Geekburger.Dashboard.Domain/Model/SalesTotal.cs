using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Domain.Model
{
    public class SalesTotal
    {
        public string? StoreName { get; set; }

        public double Value { get; set; }
        
        public int Total{ get; set; }

    }
}
