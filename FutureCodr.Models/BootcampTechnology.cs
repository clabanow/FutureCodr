using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class BootcampTechnology
    {
        [Required]
        public int? BootcampID { get; set; }

        public int BootcampTechnologyID { get; set; }

        [Required]
        public int? TechnologyID { get; set; }
    }
}
