namespace FutureCodr.UI.Models.Authentication
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("FutureCodrDev")
        {
        }
    }
}
