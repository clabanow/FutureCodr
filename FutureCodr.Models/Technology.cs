using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Technology
    {
        [Required]
        public string Name { get; set; }

        public int TechnologyID { get; set; }
    }
}
