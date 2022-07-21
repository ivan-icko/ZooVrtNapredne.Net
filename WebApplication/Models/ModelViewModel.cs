using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ModelViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public int VetId { get; set; }
        public string VetName { get; set; }

    }
}
