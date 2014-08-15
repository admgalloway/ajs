using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class GroupRelationshipService : IGroupRelationshipService
    {
        private readonly IRepository<User> userRepo;
        private readonly IApplicationService applicationService;
        private readonly IGroupService groupService;

        public GroupRelationshipService(IRepository<User> userRepo, IApplicationService applicationService, IGroupService groupService)
        {
            this.applicationService = applicationService;
            this.groupService = groupService;
            this.userRepo = userRepo;
        }

        public User SaveUserGroups(int userId, IList<int> groupIds)
        {
            var user = userRepo.Single(userId);
            user.Groups.Clear();

            if (groupIds != null && groupIds.Count > 0)
            {
                var allGroups = groupService.GetAll();
                user.Groups = allGroups.Where(a => groupIds.Contains(a.Id)).ToList();
            }

            return userRepo.Update(user);
        }

        public Group SaveGroupUsers(int groupId, IList<int> userIds)
        {
            var group = groupService.GetById(groupId);
            group.Users.Clear();
            
            if (userIds != null && userIds.Count > 0)
            {
                var allUsers = userRepo.All();
                group.Users = allUsers.Where(u => userIds.Contains(u.Id)).ToList();
            }

            return groupService.Update(group);
        }

        public Application SaveApplicationGroups(int appId, IList<int> groupIds)
        {
            var app = applicationService.GetById(appId);
            app.Groups.Clear();

            if (groupIds != null && groupIds.Count > 0)
            {
                var allGroups = groupService.GetAll();
                app.Groups = allGroups.Where(a => groupIds.Contains(a.Id)).ToList();
            }

            return applicationService.Update(app);
        }

        public Group SaveGroupApplications(int groupId, IList<int> applicationIds)
        {
            var group = groupService.GetById(groupId);
            group.Applications.Clear();
            
            if (applicationIds != null && applicationIds.Count > 0)
            {
                var allApplications = applicationService.GetAll();
                group.Applications = allApplications.Where(u => applicationIds.Contains(u.Id)).ToList();
            }

            return groupService.Update(group);
        }

    }
}