using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Extensions;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepository<User> userRepo) : base(userRepo)
	    {

	    }

        /// <summary>Get single user account using it's unique email address. returns null if it doesnt exist</summary>
        public User GetByEmail(string emailAddress)
        {
            return repo.SingleOrDefault(u => u.EmailAddress == emailAddress);
        }

        public override User Create(User user)
        {
            Validate(user);

            // generate salt key and hash password
            user.Salt = SecurityExtensions.GenerateSalt();
            user.Password = user.Password.Hash(user.Salt);
            user.DateCreated = DateTime.Now;
            return repo.Create(user);
        }

        public override User Update(User user)
        {
            Validate(user);
            
            // grab the existing user and apply new values to it
            var existingUser = GetById(user.Id);
            existingUser.ApplyValues(user);
            return repo.Update(existingUser);
        }

        public override void Delete(User obj)
        {
            if (obj != null && obj.EmailAddress == "admin@weeworld.com")
                throw new ValidationException("EmailAddress", "You cannot delete this account");

            base.Delete(obj);
        }

        public override void Validate(User user)
        {
            // if user is null, cant carry out any other validation
            if (user == null)
                throw new ValidationException("Object", "User cannot be null");

            var errors = new ValidationErrorList();

            /// check that email is provided, in correct format, and it doesnt alreadt exist
            if (string.IsNullOrEmpty(user.EmailAddress))
            { 
                errors.Add("EmailAddress", "Email Address is required");
            }
            else if (Regex.Match(user.EmailAddress, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").Success == false)
            {
                errors.Add("EmailAddress", "Email Address format is invalid");
            }
            else
            {
                var existingEmail = GetByEmail(user.EmailAddress);
                if (existingEmail != null && (existingEmail.Id != user.Id))
                {
                    errors.Add("EmailAddress", "Email Address already exists");
                }
                else if (user.Id > 0)
                {
                    // if updating a user, prevent user changing admin email address
                    var previousValues = GetById(user.Id);
                    if (previousValues.EmailAddress.ToLower() == "admin@weeworld.com" && user.EmailAddress.ToLower() != previousValues.EmailAddress.ToLower())
                    {
                        errors.Add("EmailAddress", "You cannot change the email address of this user");
                    }
                }
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                if (user.Id == 0)
                {
                    errors.Add("Password", "Password is required");
                }
            }
            else if (user.Password.Length < 4)
            {
                errors.Add("Password", "Password must be at least 4 characters");
            }

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }

    }
}