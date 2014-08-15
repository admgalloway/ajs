using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IGroupService : IBaseService<Group>
    {
        Group GetByName(string name);
    }
}