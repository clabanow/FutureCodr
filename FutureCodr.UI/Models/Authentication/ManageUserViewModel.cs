namespace FutureCodr.UI.Models.Authentication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ManageUserViewModel
    {
        [Compare("NewPassword", ErrorMessage="The new password and confirmation password do not match."), DataType(DataType.Password), Display(Name="Confirm new password")]
        public string ConfirmPassword { get; set; }

        [StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6), Display(Name="New password"), Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name="Current password"), Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
