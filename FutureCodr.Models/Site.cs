using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Site
    {
        public int SiteID { get; set; }

        [Required]
        public string SiteLogoURL { get; set; }

        [Required]
        public string SiteName { get; set; }
    }
}
