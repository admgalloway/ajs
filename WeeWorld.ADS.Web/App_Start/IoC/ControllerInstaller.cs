using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace WeeWorld.ADS.Web.IoC
{
    /// <summary>Controller configuration to hook into IoC container.
    /// </summary>
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // register all mvc controllers
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            
            // register all api controllers
            container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest()); 

        }
    }
}