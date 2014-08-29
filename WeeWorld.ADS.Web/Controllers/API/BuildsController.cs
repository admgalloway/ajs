using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.Web.ADS.Controllers.API.Attributes;

namespace WeeWorld.ADS.Web.Controllers.API
{
    [RoutePrefix("api/builds")]
    public class BuildsController : BaseController<Build>
    {
        private readonly IBuildService buildService;
        private IApplicationService applicationService;

        public BuildsController(IBuildService buildService, IApplicationService applicationService)
        {
            this.buildService = buildService;
            this.applicationService = applicationService;
        }

        /// <summary>Get a collection of all builds</summary>
        [RequireToken]
        public IEnumerable<Build> GetAll()
        {
            return buildService.GetAll();
        }

        [Route("~/api/applications/{appId:int}/builds")]
        [RequireToken]
        public IList<Build> GetByApp(int appId)
        {
            return buildService.GetByApp(appId).ToList();
        }

        [Route("~/api/applications/{appId:int}/builds/struct")]
        [RequireToken]
        public IDictionary<int, IList<int>> GetBuildSctructure(int appId)
        {
            return buildService.GetBuildStructure(appId);
        }

        /// <summary>Update an existing Application</summary>
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage Put(int id, Build build)
        {
            var response = buildService.Update(build);
            return ResourceWith200(response);
        }
    }
}