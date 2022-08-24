using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ValidateYearsAttribute : ValidationAttribute
    {
        private readonly DateTime _minValue = DateTime.UtcNow;
        private readonly DateTime _maxValue = DateTime.UtcNow.AddYears(30);

        public override bool IsValid(object value)
        {
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Greska", _minValue, _maxValue);
        }
    }


    

    public class PackageViewModel
    {
        

        public  int PackageId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public double Duration { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public int FreePlaces { get; set; }


        //[Range(typeof(DateTime), "12/12/2063 12:00:00 AM", "12/12/2063 12:00:00 AM",ErrorMessage = "Greska")]
        [ValidateYears]
        public DateTime DateTime { get; set; }
        public List<SelectListItem> Animals { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> OtherAnimals { get; set; } = new List<SelectListItem>();

        public List<int> NewAnimalsInPackage { get; set; } = new List<int>();

    }
}
