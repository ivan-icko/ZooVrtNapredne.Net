using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }

    }
}
