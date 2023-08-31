using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Detai
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Filter", "{type}/{meta}",
                new { controller = "Filter", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "category" }
                },
                namespaces: new[] { "Detai.Controllers" });

            routes.MapRoute("Detail", "{type}/{meta}/{id}",
                new { controller = "Filter", action = "Detail", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "category" }
                },
                namespaces: new[] { "Detai.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
