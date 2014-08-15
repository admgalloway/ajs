using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WeeWorld.ADS.Data;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Data.Repositories.Concrete;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Services.Concrete;

namespace MCMS.Services.Configuration
{
    /// <summary>Register all servicesso that Windsor can resolve them as dependancies</summary>
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Declare concrete Application service
            container.Register(Component.For<DatabaseContext>().ImplementedBy<DatabaseContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(EFRepository<>)).LifeStyle.PerWebRequest);

        }
    }
}