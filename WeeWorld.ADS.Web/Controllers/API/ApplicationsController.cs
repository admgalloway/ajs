using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.Web.ADS.Controllers.API.Attributes;

namespace WeeWorld.ADS.Web.Controllers.API
{
    [RoutePrefix("api/applications")]
    public class ApplicationsController : BaseController<Application>
    {
        private readonly IApplicationService applicationService;
        private readonly IGroupRelationshipService groupRelationshipService;
        private readonly IStorageService storageService;
        private readonly IBuildService buildService;
        private readonly IUserService userService;

        public ApplicationsController(IApplicationService applicationService, IGroupRelationshipService groupRelationshipService, IStorageService storageService, IBuildService buildService, IUserService userService)
        {
            this.applicationService = applicationService;
            this.groupRelationshipService = groupRelationshipService;
            this.storageService = storageService;
            this.buildService = buildService;
            this.userService = userService;
        }

        /// <summary>Get a single Application using its unique id</summary>
        [RequireToken]
        public Application Get(int id)
        {
            return applicationService.GetById(id);
        }

        /// <summary>Get an Application, identified by its unique name</summary>
        [RequireToken]
        public Application GetByName(string name)
        {
            return applicationService.GetByName(name);
        }

        /// <summary>Get a collection of all Applications</summary>
        [RequireToken]
        public IEnumerable<Application> GetAll()
        {
            return applicationService.GetAll();
        }

        [Route("~/api/users/{userId:int}/applications")]
        [RequireToken]
        public IEnumerable<Application> GetByUser(int userId)
        {
            var user = userService.GetById(userId); 
            return user.Applications;
        }

        /// <summary>Create a new Application</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Post(Application application)
        {
            var response = applicationService.Create(application);

            return ResourceWith201(response);
        }

        /// <summary>Update an existing Application</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Put(int id, Application application)
        {
            var response = applicationService.Update(application);
            return ResourceWith200(response);
        }

        /// <summary>Delete an Application</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Delete(int id)
        {
            applicationService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }


        [RequireToken(asAdministrator: true)]
        [Route("{id:int}/groups", Order = 1)]
        public HttpResponseMessage PutGroups(int id, [FromBody]IList<int> groups)
        {
            var response = groupRelationshipService.SaveApplicationGroups(id, groups);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}