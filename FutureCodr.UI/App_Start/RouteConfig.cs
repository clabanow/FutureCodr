using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FutureCodr.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Bootcamp", "Bootcamp/{id}", new { controller = "Bootcamp", action = "Index" });
            routes.MapRoute("Location", "location/{id}", new { controller = "Home", action = "Location" });
            routes.MapRoute("Technology", "technology/{id}", new { controller = "Home", action = "Technology" });
            routes.MapRoute("Contact", "Contact/", new { controller = "Home", action = "Contact" });
            routes.MapRoute("Success", "Contact/Success", new { controller = "Home", action = "Success" });
            routes.MapRoute("Home", "/", new { controller = "Home", action = "Index" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
