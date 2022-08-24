using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class VetViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string VName { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string JMBG { get; set; }
    }
}
