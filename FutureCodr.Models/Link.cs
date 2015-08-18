using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Link
    {
        [Required]
        public int? BootcampID { get; set; }

        [DataType(DataType.Date), Required]
        public DateTime Date { get; set; }

        public int LinkID { get; set; }

        [Required]
        public string LinkText { get; set; }

        [Required]
        public int? SiteID { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
