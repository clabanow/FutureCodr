namespace FutureCodr.UI.Models.Authentication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoginViewModel
    {
        [Required(ErrorMessage="Enter a Password"), DataType(DataType.Password), Display(Name="Password")]
        public string Password { get; set; }

        [Display(Name="Remember me?")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage="Enter a Username"), Display(Name="User name")]
        public string UserName { get; set; }
    }
}
