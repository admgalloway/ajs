using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WeeWorld.ADS.Controllers.API.Attributes
{
    /// <summary>Force requests over HTTPS</summary>
    public class RequiresHttpsAttribute : AuthorizationFilterAttribute
    {
        /// <summary>Overriding OnAuthorization to force all API requests over https</summary>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool requiresHttps = Boolean.Parse(ConfigurationManager.AppSettings["RequireHttps"]);
            bool usingHttps = actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps;

            if (requiresHttps && !usingHttps)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Request must be made over https" };
        }
    }
}