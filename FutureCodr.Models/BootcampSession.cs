using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class BootcampSession
    {
        [Required]
        public int? BootcampID { get; set; }

        public int BootcampSessionID { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int? LocationID { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public int? TechnologyID { get; set; }
    }
}
