using System.Web.Http;
using WeeWorld.ADS.Controllers.API.Attributes;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Services.Concrete;
using WeeWorld.ADS.ViewModels.Auth;

namespace WeeWorld.ADS.Controllers.API
{
    [ValidationExceptionFilter]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IMembershipService membershipService;
        private readonly ITokenService tokenService;

        public AuthController(IMembershipService membershipService, ITokenService tokenService)
        {
            this.membershipService = membershipService;
            this.tokenService = tokenService;
        }

        public IHttpActionResult PostLogin(LoginVM credentials)
        {
            if (credentials == null)
            {
                return BadRequest("Invalid user credentials");
            }

            User user;
            var result = membershipService.AuthenticateUser(credentials.EmailAddress, credentials.Password, out user);

            if (result == AuthenticationResult.Failed)
            {
                return BadRequest("Invalid user credentials");
            }
            else
            {
                // generate token, append to url as fragment
                var token = tokenService.Create(user);
                return Ok<Token>(token);
            }
        }
    }
}