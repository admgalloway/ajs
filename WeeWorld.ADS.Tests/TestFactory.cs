using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Tests
{
    public static class TestFactory
    {

        public static User User(int id = 1)
        {
            return new User
            {
                Id = id,
                EmailAddress = string.Format("email-{0}@email.com", id),
                Password = "pword",
                Salt = new byte[] { 0, 1, 2, 3, 4, 5 }
            };
        }

        public static Application App(int id = 1)
        {
            return new Application
            {
                Id = id,
                Name = "Name-Test-" + id
            };
        }

        public static Group Group(int id = 1)
        {
            return new Group
            {
                Id = id,
                Name = "Name-Test-" + id
            };
        }

        public static Group AdminGroup()
        {
            return new Group
            {
                Id = 999,
                Name = "Administrators"
            };
        }

        public static Token Token(int id = 1)
        {
            return new Token
            {
                Id = id,
                Code = "Code-Test-" + id,
                ExpiryDate = DateTime.Now.AddHours(1)
            };
        }

    }
}
