using System;
using System.Linq;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Models.Validation;

namespace WeeWorld.ADS.Services.Concrete
{
    public class TokenService : BaseService<Token>, ITokenService
    {
        public TokenService(IRepository<Token> tokenRepo) : base(tokenRepo)
	    {

	    }

        /// <summary>Get single user account using it's unique email address. returns null if it doesnt exist</summary>
        public Token GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
                
            return repo.SingleOrDefault(u => u.Code == code);
        }

        /// <summary>Get a list of active tokens for this user</summary>
        public IList<Token> GetByUser(int userId)
        {
            if (userId < 1)
                return new List<Token>();

            return repo.List(t => t.User.Id == userId && t.ExpiryDate > DateTime.Now).ToList();
        }

        /// <summary>Get a list of active tokens for this user</summary>
        private Token GetCurrentForUser(int userId)
        {
            var tokens = GetByUser(userId);

            return tokens.OrderByDescending(t => t.ExpiryDate).FirstOrDefault();
        }


        /// <summary>Get a list of active tokens for this user, identified by their email address</summary>
        public IList<Token> GetByEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) 
                return new List<Token>();

            return repo.List(t => t.User.EmailAddress == emailAddress && t.ExpiryDate > DateTime.Now).ToList();
        }

        public Token Create(User user)
        {
            // get latest token for this user...
            var token = GetCurrentForUser(user.Id);

            // if its no longer valid, generate a new one
            if (token == null)
            {
                token = new Token {
                    User = user,
                    ExpiryDate = DateTime.Now.AddHours(2),
                    Code = Guid.NewGuid().ToString().Replace("-", "")
                };
                base.Create(token);
            }

            return token;
        }

        public override void Validate(Token token)
        {
            // if token is null, cant carry out any other validation
            if (token == null)
                throw new ValidationException("Object", "Token cannot be null");

            var errors = new ValidationErrorList();

            // ...
        }

        /// <summary>Validate a token code</summary>
        public bool IsCodeValid(string code, bool requriesAdmin = false)
        {
            Token token;
            return IsCodeValid(code, out token, requriesAdmin);
        }

            /// <summary>Validate a token code</summary>
        public bool IsCodeValid(string code, out Token token, bool requriesAdmin = false)
        {
            if (code == null)
            {
                token = null;
                return false;
            }

            token = GetByCode(code);

            // check if token has expired
            if (token == null || token.Expired)
            {
                token = null;
                return false;
            }

            // check if token needs to have been granted to an admin user
            if (requriesAdmin && token.User.IsAdmin == false) 
            {
                token = null;
                return false;
            }

            return true;
        }
    }
}