namespace FutureCodr.UI.Models.Authentication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RegisterViewModel
    {
        [Compare("Password", ErrorMessage="Does not match password."), DataType(DataType.Password), Display(Name="Confirm password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(100, ErrorMessage="Password must be at least {2} characters.", MinimumLength=6), Display(Name="Password"), Required(ErrorMessage="Enter a Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="Enter a Username"), Display(Name="User name")]
        public string UserName { get; set; }
    }
}
