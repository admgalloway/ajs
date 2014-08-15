using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Controllers.API.Attributes;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Controllers.API
{
    [RequiresHttps]
    public class FilesController : ApiController
    {
        private readonly IFileService fileService;

        public FilesController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [Route("api/refresh")]
        [RequireToken(asAdministrator: true)]
        public HttpResponseMessage PostRefresh(bool apps = true, bool builds = true, bool fullScan = false)
        {
            if (apps == true)
                fileService.LookForApplications();

            if (builds == true)
                fileService.LookForBuilds(fullScan);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}