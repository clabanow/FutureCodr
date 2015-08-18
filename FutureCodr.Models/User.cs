using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserID { get; set; }
    }
}
