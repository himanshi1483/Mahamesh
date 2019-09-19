using System.Web.Mvc;
using System.Web.Routing;

namespace Mahamesh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         //   routes.MapRoute("AppData", "{controller}/{action}", new { controller = "ApplicantRegistrations", action = "GetAplications" });
            routes.MapRoute(
                  name: "Language",
                  url: "{lang}/{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                  constraints: new { lang = @"mr" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "en" }
            );
        }
    }
}
