using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Domain
{
    public class UserWithLessOffer
    {
        public int UserId { get; set; }
        public List<string> Restrictions { get; set; } = new();
    }
}
