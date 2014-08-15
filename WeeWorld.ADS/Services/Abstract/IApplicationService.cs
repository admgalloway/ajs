using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IApplicationService : IBaseService<Application>
    {
        Application GetByName(string name);
    }
}