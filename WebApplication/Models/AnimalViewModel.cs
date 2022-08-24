using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class AnimalViewModel
    {
        [Required(ErrorMessage = "Type not good")]
        public string Type { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Number not good")]
        public int Age { get; set; }
        public List<SelectListItem> Vets { get; set; } = new List<SelectListItem>();
        public int VetId { get; set; }
        public int AnimalId { get;  set; }
        public string VetName { get;  set; }
        //public string VName { get; set; }
        public string ImagePath { get; set; }
    }
}
