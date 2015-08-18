using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class Review
    {
        [Required]
        public int? BootcampID { get; set; }

        public bool IsVerified { get; set; }

        public int ReviewID { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int? UserID { get; set; }
    }
}
