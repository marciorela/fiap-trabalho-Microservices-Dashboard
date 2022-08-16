using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Domain.Entities
{
    public class Order
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string? StoreName { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
