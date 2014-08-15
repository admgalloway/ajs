using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Concrete
{
    public interface IMembershipService
    {
        AuthenticationResult AuthenticateUser(string email, string password);

        AuthenticationResult AuthenticateUser(string email, string password, out User user);
    }
}