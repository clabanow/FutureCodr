namespace FutureCodr.UI.Models.Authentication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ExternalLoginConfirmationViewModel
    {
        [Display(Name="User name"), Required]
        public string UserName { get; set; }
    }
}
