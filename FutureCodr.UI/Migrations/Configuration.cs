namespace FutureCodr.UI.Migrations
{
    using FutureCodr.UI.Models.Authentication;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            base.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> manager2 = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserValidator<ApplicationUser> userValidator = manager.UserValidator as UserValidator<ApplicationUser>;
            if (userValidator != null)
            {
                userValidator.AllowOnlyAlphanumericUserNames = false;
            }
            if (!manager2.RoleExists<IdentityRole>("admin"))
            {
                manager2.Create<IdentityRole>(new IdentityRole("admin"));
                manager2.Create<IdentityRole>(new IdentityRole("user"));
                ApplicationUser user = new ApplicationUser {
                    UserName = "clabanow@gmail.com"
                };
                manager.Create<ApplicationUser>(user, "charles");
                manager.AddToRole<ApplicationUser>(user.Id, "admin");
            }
        }
    }
}
