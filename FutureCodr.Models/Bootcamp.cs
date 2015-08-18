using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Bootcamp
    {
        public int BootcampID { get; set; }

        [Required, Range(1, 0x34)]
        public int LengthInWeeks { get; set; }

        [Required]
        public int LocationID { get; set; }

        [Required]
        public string LogoLink { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int PrimaryTechnologyID { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
