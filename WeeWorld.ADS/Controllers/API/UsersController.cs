using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Controllers.API.Attributes;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Controllers.API
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController<User>
    {
        private readonly IUserService userService;
        private readonly IGroupRelationshipService groupRelationshipService;

        public UsersController(IUserService userService, IGroupRelationshipService groupRelationshipService)
        {
            this.userService = userService;
            this.groupRelationshipService = groupRelationshipService;
        }

        /// <summary>Get a single User using their unique id</summary>
        [RequireToken]
        public IHttpActionResult Get(int id)
        {
            return Ok<User>(userService.GetById(id));
        }

        /// <summary>Get a user, identified by their unique email address</summary>
        [RequireToken]
        public User GetByEmail(string emailAddress)
        {
            return userService.GetByEmail(emailAddress);
        }

        /// <summary>Get a collection of all Users</summary>
        //[RequireToken]
        public IEnumerable<User> GetAll()
        {
            return userService.GetAll();
        }

        /// <summary>Create a new User</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Post(User user)
        {
            var response = userService.Create(user);

            return ResourceWith201(response);
        }

        /// <summary>Update an existing User</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Put(int id, User user)
        {
            var response = userService.Update(user);
            return ResourceWith200(response);
        }

        /// <summary>Delete an User</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Delete(int id)
        {
            userService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [RequireToken(asAdministrator: true)]
        [Route("{id:int}/groups", Order = 1)]
        public HttpResponseMessage PutGroups(int id, [FromBody]IList<int> groups)
        {
            var response = groupRelationshipService.SaveUserGroups(id, groups);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}