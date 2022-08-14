using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Vet
    {
        public int VetId { get; set; }
        public string VName { get; set; }
       // public string LastName { get; set; }
        public DateTime DateOfBIrth { get; set; }
        public string PhoneNumber { get; set; }
        public string JMBG { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
