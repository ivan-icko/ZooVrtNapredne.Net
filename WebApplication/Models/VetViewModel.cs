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
        public Animal Animal { get; set; }
        public List<SelectListItem> Animals { get; set; }
        public string VName { get; set; }
    }
}
