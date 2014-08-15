using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WeeWorld.ADS.Extensions
{

    /// <summary>Provides common security functions (ie psasword hashing etc)</summary>
    public static class SecurityExtensions
    {
        private static SHA384CryptoServiceProvider sha3Service;
        private readonly static RNGCryptoServiceProvider rngService;

        static SecurityExtensions()
        {
            rngService = new RNGCryptoServiceProvider();
            sha3Service = new SHA384CryptoServiceProvider();
        }

        /// <summary>Has a value and salt key and return string representation of the result</summary>
        public static string Hash(this string source, byte[] salt)
        {
            // convert source to byte array
            var sourceBytes = UnicodeEncoding.UTF8.GetBytes(source);

            // create new array combining source bytes and salt
            var SaltAndSource = new byte[sourceBytes.Length + salt.Length];
            salt.CopyTo(SaltAndSource, 0);
            sourceBytes.CopyTo(SaltAndSource, salt.Length);

            // hash the combined value and return string representation
            var hash = sha3Service.ComputeHash(SaltAndSource);
            return Convert.ToBase64String(hash);
        }

        /// <summary>Generate a random salt key</summary>
        public static byte[] GenerateSalt()
        {
            byte[] data = new byte[8];
            rngService.GetBytes(data);
            return data;
        }

    }
}