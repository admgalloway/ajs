using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IBuildService : IBaseService<Build>
    {
        IEnumerable<Build> GetByApp(int appId);

        Build GetByVersionNumber(int appId, string buildNumber);

        IDictionary<int, IList<int>> GetBuildStructure(int appId);

    }
}