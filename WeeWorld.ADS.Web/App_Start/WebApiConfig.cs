using System.Web.Http;
using WeeWorld.ADS.Web.Controllers.API.Attributes;
using WeeWorld.ADS.Web.Formatters;

namespace WeeWorld.ADS.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // disable support for xml; json only
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Insert(0, new IModelFormatter());

            // set default options for serlialising loops
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            config.Filters.Add(new ValidationExceptionFilterAttribute());
        }

    }
}