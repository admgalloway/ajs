using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Services.Concrete;

namespace MCMS.Services.Configuration
{
    /// <summary>Register all servicesso that Windsor can resolve them as dependancies</summary>
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Declare concrete Application service
            container.Register(Component.For<IApplicationService>().ImplementedBy<ApplicationService>().LifestylePerWebRequest());
            container.Register(Component.For<IGroupService>().ImplementedBy<GroupService>().LifestylePerWebRequest());
            container.Register(Component.For<IGroupRelationshipService>().ImplementedBy<GroupRelationshipService>().LifestylePerWebRequest());
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            container.Register(Component.For<ITokenService>().ImplementedBy<TokenService>().LifestylePerWebRequest());
            container.Register(Component.For<IMembershipService>().ImplementedBy<MembershipService>().LifestylePerWebRequest());
            container.Register(Component.For<IBuildService>().ImplementedBy<BuildService>().LifestylePerWebRequest());
            container.Register(Component.For<IStorageService>().ImplementedBy<AzureStorageService>().LifestylePerWebRequest());
            container.Register(Component.For<IFileService>().ImplementedBy<FileService>().LifestylePerWebRequest());
        }
    }
}