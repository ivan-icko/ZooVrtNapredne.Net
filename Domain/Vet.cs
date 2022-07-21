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
        public String VName { get; set; }

        public List<Animal> Animals { get; set; }
    }
}
