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
                name: "License/CheckLicense",
                url: "License/CheckLicense",
                defaults: new { controller = "License", action = "CheckLicense", stUrl = UrlParameter.Optional }
            );
            routes.MapRoute(
                   name: "search",
                   url: "Search",
                   defaults: new { controller = "Home", action = "Search", stUrl = UrlParameter.Optional }
               );
            routes.MapRoute(
                  name: "SendMail",
                  url: "SendMail",
                  defaults: new { controller = "Home", action = "SendMail", stUrl = UrlParameter.Optional }
              );
            routes.MapRoute(
                  name: "SearchTags",
                  url: "SearchTags",
                  defaults: new { controller = "Home", action = "SearchTags", stUrl = UrlParameter.Optional }
              );
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
