using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class BootcampLocation
    {
        [Required]
        public int BootcampID { get; set; }

        public int BootcampLocationID { get; set; }

        [Required]
        public int LocationID { get; set; }
    }
}
