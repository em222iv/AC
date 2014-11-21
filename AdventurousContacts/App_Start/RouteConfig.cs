using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdventurousContacts
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "contact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "404-PageNotFound",
                "{controller}/{action}",
                new { controller = "Shared", action = "_errorPage" }
            );
        }
    }
}