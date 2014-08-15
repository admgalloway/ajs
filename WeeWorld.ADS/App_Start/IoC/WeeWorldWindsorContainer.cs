using Castle.Windsor;
using Castle.Windsor.Installer;

namespace WeeWorld.ADS.IoC
{
    /// <summary>A custom WindsorContainer configured to install any WeeWorld dependencies</summary>
    public class WeeWorldWindsorContainer : WindsorContainer
    {
        public WeeWorldWindsorContainer()
        {
            // Install any components declared in the web config first. Allows custom configuration
            // Install(Configuration.FromAppConfig());
            
            Install(FromAssembly.This());
        }
    }
}