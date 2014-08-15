using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        User GetByEmail(string emailAddress);
    }
}