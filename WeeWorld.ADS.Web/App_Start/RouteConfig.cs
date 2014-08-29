using System.Web.Mvc;
using System.Web.Routing;

namespace WeeWorld.ADS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.handlebars/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}",
                defaults: new { controller = "App", action = "Index" }                
            );
        }
    }
}