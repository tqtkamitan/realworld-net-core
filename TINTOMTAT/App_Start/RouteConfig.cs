using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TINTOMTAT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PostDetail",
                url: "{alias}.html",
                defaults: new { controller = "Post", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "TINTOMTATs.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Category",
                url: "{Category}/{alias}.html",
                defaults: new { controller = "Category", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "TINTOMTATs.Web.Controllers" }
            );

            routes.MapRoute(
               name: "admin",
               url: "admin/{id}",
               defaults: new { controller = "HomeAdmin", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "TINTOMTATs.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Trang chủ",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TINTOMTATs.Web.Controllers" }
           );

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "TINTOMTATs.Web.Controllers" }
            );
        }
    }
}
