using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Controllers.API.Attributes;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Controllers.API
{
    [RoutePrefix("api/groups")]
    public class GroupsController : BaseController<Group>
    {
        private readonly IGroupService groupService;
        private readonly IGroupRelationshipService groupRelationshipService;

        public GroupsController(IGroupService groupService, IGroupRelationshipService groupRelationshipService)
        {
            this.groupService = groupService;
            this.groupRelationshipService = groupRelationshipService;
        }

        /// <summary>Get a single Group using its unique id</summary>
        [RequireToken]
        public Group Get(int id)
        {
            return groupService.GetById(id);
        }

        /// <summary>Get an Group, identified by its unique name</summary>
        [RequireToken]
        public Group GetByName(string name)
        {
            return groupService.GetByName(name);
        }

        /// <summary>Get a collection of all Groups</summary>
        [RequireToken]
        public IEnumerable<Group> GetAll()
        {
            return groupService.GetAll();
        }

        /// <summary>Create a new Group</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Post(Group group)
        {
            var response = groupService.Create(group);

            return ResourceWith201(response);
        }

        /// <summary>Update an existing Group</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Put(int id, Group group)
        {
            var response = groupService.Update(group);
            return ResourceWith200(response);
        }

        /// <summary>Delete an Group</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Delete(int id)
        {
            groupService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }


        [RequireToken(asAdministrator: true)]
        [Route("{id:int}/users", Order = 1)]
        public HttpResponseMessage PutUsers(int id, [FromBody]IList<int> users)
        {
            var response = groupRelationshipService.SaveGroupUsers(id, users);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [RequireToken(asAdministrator: true)]
        [Route("{id:int}/applications", Order = 1)]
        public HttpResponseMessage PutApplications(int id, [FromBody]IList<int> applications)
        {
            var response = groupRelationshipService.SaveGroupApplications(id, applications);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}