using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Extensions
{
    /// <summary>Provides common security functions (ie psasword hashing etc)</summary>
    public static class MappingExtensions
    {
        /// <summary>Replace properties on source with those from target</summary>
        public static void ApplyValues(this User source, User newValues)
        {
            if (source == null || newValues == null)
            {
                return;
            }
            source.EmailAddress = newValues.EmailAddress;

            // check if a new password is provided
            if (newValues.Password != null)
            {
                // create new salt key for each new password
                source.Salt = SecurityExtensions.GenerateSalt();
                source.Password = newValues.Password.Hash(source.Salt);
            }
        }
    }
}