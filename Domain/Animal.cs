using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Animal
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public string ImagePath { get; set; }
        public List<Package> Packages { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Animal animal &&
                    //Id == animal.Id &&
                   Type == animal.Type &&
                   Age == animal.Age &&
                   VetId == animal.VetId;
        }
    }
}
