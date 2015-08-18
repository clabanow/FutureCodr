using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FutureCodr.Models
{
    public class ContactForm
    {
        public int ContactFormID { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
    }
}
