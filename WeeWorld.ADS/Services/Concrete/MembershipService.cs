using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Extensions;

namespace WeeWorld.ADS.Services.Concrete
{
    public class MembershipService : IMembershipService
    {
        private readonly IUserService userService;

        public MembershipService(IUserService userService)
	    {
            this.userService = userService;
	    }

        /// <summary>Validate a set of user credentials</summary>
        public AuthenticationResult AuthenticateUser(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(password))
                return AuthenticationResult.Failed;

            var user = userService.GetByEmail(emailAddress);

            if (user == null)
                return AuthenticationResult.Failed;

            // has the suppleid password and verify it matches the stored record
            var hashedPassword = password.Hash(user.Salt);
            if (user.Password != hashedPassword)
                return AuthenticationResult.Failed;

            // all tests passed, return success
            return AuthenticationResult.Success;
        }

        /// <summary>Validate a set of user credentials, and return the user obejct if valid</summary>
        public AuthenticationResult AuthenticateUser(string emailAddress, string password, out User user)
        {
            user = null;

            var result = AuthenticateUser(emailAddress, password);

            if (result == AuthenticationResult.Success)
                user = userService.GetByEmail(emailAddress);

            return result;
        }



    }
}