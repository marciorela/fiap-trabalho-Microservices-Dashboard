using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Domain.Entities
{
    public class Restriction
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = "";
    }
}
