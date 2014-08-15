using System.Collections.Generic;
using System.Web.Http;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Controllers.API
{
    [RoutePrefix("api/tokens")]
    public class TokensController : BaseController<Token>
    {
        private readonly ITokenService tokenService;

        public TokensController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        /// <summary>Get a single token using its unique id</summary>
        public IEnumerable<Token> GetAll()
        {
            return tokenService.GetAll();
        }

        /// <summary>Get a single token using its unique id</summary>
        [Route("{id:int}")]
        public Token Get(int id)
        {
            return tokenService.GetById(id);
        }

        /// <summary>Get a single token using its unique id</summary>
        [Route("{code}")]
        public Token GetByCode(string code)
        {
            return tokenService.GetByCode(code);
        }

        /// <summary>Get a token, identified by its unique code</summary>
        [Route("verify")]
        public dynamic PostVerify([FromBody] dynamic body)
        {
            if (body == null || body.code == null)
                return new { valid = false };

            var token = tokenService.GetByCode((string)body.code);

            if (token == null || token.Expired)
                return new { valid = false };

            return new {
                valid = true,
                token = new {
                    Code = token.Code,
                    Expires = token.ExpiryDate,
                    User = token.User.Id,
                    Admin = token.Admin
                }
            };
        }

        /// <summary>Get a collection of all tokens allocated to a user</summary>
        public IEnumerable<Token> GetByUser(int userId)
        {
            return tokenService.GetByUser(userId);
        }

    }
}