using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewNationals
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                       name: "stUrl",
                       url: "{stUrl}",
                       defaults: new { controller = "Home", action = "Default", stUrl = UrlParameter.Optional }
                   );
            routes.MapRoute(
                          name: "stUrl/page",
                          url: "{stUrl}/{page}",
                          defaults: new { controller = "Home", action = "Default", stUrl = UrlParameter.Optional, Page = UrlParameter.Optional }
                      );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
