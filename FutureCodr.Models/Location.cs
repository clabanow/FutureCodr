using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Location
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public int LocationID { get; set; }
    }
}
