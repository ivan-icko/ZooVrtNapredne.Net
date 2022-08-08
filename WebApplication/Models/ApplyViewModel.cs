using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ApplyViewModel
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
        public string  UserLastName { get; set; }
        public int NumerOfPersons { get; set; } = 1;

        public List<SelectListItem> Animals { get; set; } = new List<SelectListItem>();

    }
}
