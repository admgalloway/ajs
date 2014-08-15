using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using WeeWorld.ADS.IoC;

namespace WeeWorld.ADS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class WebApp : HttpApplication
    {
        private static WeeWorldWindsorContainer container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register); 
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            SetupIoC(GlobalConfiguration.Configuration);
        }

        /// <summary>Setup our IoC container and register a custom controllerFactory to use it
        /// </summary>
        private void SetupIoC(HttpConfiguration config)
        {

            // setup a windsor container using any installers declared in this assembly
            container = new WeeWorldWindsorContainer();

            // replace default Controller Factory with dedicated WeeWorld one (for MVC Controllers)
            //var controllerFactory = new WindsorControllerFactory(container.Kernel);
            //ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // replace default Controller Activator with dedicated WeeWorld one (for API controllers)
            var controllerActivator = new WindsorControllerActivator(container);
            config.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}