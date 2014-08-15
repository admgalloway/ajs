using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IGroupRelationshipService
    {
        User SaveUserGroups(int userId, IList<int> groupIds);

        Group SaveGroupUsers(int groupId, IList<int> userIds);

        Application SaveApplicationGroups(int appId, IList<int> groupIds);

        Group SaveGroupApplications(int groupId, IList<int> applicationIds);
    }
}