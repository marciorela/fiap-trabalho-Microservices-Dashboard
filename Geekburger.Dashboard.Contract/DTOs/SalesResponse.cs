using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Contract.DTOs
{
    public class SalesResponse
    {
        public string StoreName { get; set; }
        public int Total { get; set; }
        public double Value { get; set; }
    }
}
