namespace FutureCodr.UI
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using System;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    CookieAuthenticationOptions options = new CookieAuthenticationOptions
        //    {
        //        AuthenticationType = "ApplicationCookie",
        //        LoginPath = new PathString("/Account/Login")
        //    };
        //    app.UseCookieAuthentication(options);
        //    app.UseExternalSignInCookie("ExternalCookie");
        //}
    }
}
