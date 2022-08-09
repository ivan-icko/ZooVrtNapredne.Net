using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PU
    {
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime TimeOfReservation { get; set; }
        
    }
}
