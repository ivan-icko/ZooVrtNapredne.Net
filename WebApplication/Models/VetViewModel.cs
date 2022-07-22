using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class VetViewModel
    {
        public string Type { get; set; }
        public int Age { get; set; }
        public List<SelectListItem> Vets { get; set; } = new List<SelectListItem>();
        public int VetId { get; set; }
        public int AnimalId { get;  set; }
        public string VetName { get;  set; }
        //public string VName { get; set; }
    }
}
