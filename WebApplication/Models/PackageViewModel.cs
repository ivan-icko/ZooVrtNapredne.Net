using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<SelectListItem> Animals { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> OtherAnimals { get; set; } = new List<SelectListItem>();

        public List<int> NewAnimalsInPackage { get; set; }

    }
}
