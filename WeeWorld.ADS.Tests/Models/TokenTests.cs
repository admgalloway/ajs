using System;
using System.Linq;
using NUnit.Framework;

namespace WeeWorld.ADS.Tests.Models
{
    [TestFixture]
    public class TokenTests
    {
        [Test]
        public void Test_Expired_Returns_True_When_Expiry_Date_Has_Passed()
        {
            /// Arrange
            var token = TestFactory.Token(1);
            token.ExpiryDate = DateTime.Now.AddHours(-5);

            // Act
            var expired = token.Expired;

            // Assert
            Assert.True(expired);
        }

        [Test]
        public void Test_Expired_Returns_False_When_Expiry_Date_Is_In_Future()
        {
            /// Arrange
            var token = TestFactory.Token(1);
            token.ExpiryDate = DateTime.Now.AddHours(5);

            // Act
            var expired = token.Expired;

            // Assert
            Assert.False(expired);
        }

        [Test]
        public void Test_That_Admin_Returns_True_If_User_In_Admin_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);
            var grp = TestFactory.AdminGroup();

            user.Groups.Add(grp);
            var token = TestFactory.Token(1);
            token.User = user;
            
            // Assert
            Assert.True(token.Admin);
        }

        [Test]
        public void Test_That_Admin_Returns_False_If_User_Not_In_Admin_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);
            var token = TestFactory.Token(1);

            token.User = user;

            // Assert
            Assert.False(token.Admin);
        }

        [Test]
        public void Test_That_Admin_Returns_False_If_User_Is_Null()
        {
            // Arrange (Act)
            var token = TestFactory.Token(1);

            // Assert
            Assert.False(token.Admin);
        }

    }
}
