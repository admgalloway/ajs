using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WeeWorld.ADS.Data;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Concrete;
using WeeWorld.ADS.Services.Concrete;

namespace WeeWorld.Web.ADS.Controllers.API.Attributes
{
    /// <summary>
    /// Enforces the inclusion of an access token that verifies requests are authentic. 
    /// The token supplied must have been granted to the resource owner, or to a user
    /// in one of the supplied roles. This attribute also enforceds 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireTokenAttribute : ActionFilterAttribute
    {
        private readonly bool asAdministrator;
        private readonly TokenService tokenService;

        public RequireTokenAttribute(bool asAdministrator = false)
        {
            this.asAdministrator = asAdministrator;
            this.tokenService = new TokenService(new EFRepository<Token>(new DatabaseContext())); 
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //return;

            if (actionContext.Request.Headers.Authorization == null)
            {
                throw new UnauthorizedAccessException("Authorization header not detected");
            }

            // grab the code from the header
            var code = actionContext.Request.Headers.Authorization.ToString();

            // check if its valid
            bool valid = tokenService.IsCodeValid(code, asAdministrator);

            // throw an exzception if its not
            if (!valid)
            {
                throw new UnauthorizedAccessException("code not valid");
            }

        }
    }
}