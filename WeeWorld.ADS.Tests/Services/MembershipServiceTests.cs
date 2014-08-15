using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;
using WeeWorld.ADS.Services.Concrete;
using Moq;
using NUnit.Framework;

namespace WeeWorld.ADS.Tests.Services
{
    [TestFixture]
    public class MembershipServiceTests
    {
        private Mock<IUserService> mockUserService { get; set; }
        private MembershipService testService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockUserService = new Mock<IUserService>();
            testService = new MembershipService(mockUserService.Object);
        }

        [Test]
        public void Test_That_AuthenticateUser_Returns_Failed_When_Password_Is_Null()
        { 
            // arrange

            // act
            var result = testService.AuthenticateUser("any", null);

            // assert
            Assert.That(result, Is.EqualTo(AuthenticationResult.Failed));
        }

        [Test]
        public void Test_That_AuthenticateUser_Returns_Failed_When_EmailAdress_Is_Null()
        {
            // arrange

            // act
            var result = testService.AuthenticateUser("test@email.com", "password");

            // assert
            Assert.That(result, Is.EqualTo(AuthenticationResult.Failed));
        }

        [Test]
        public void Test_That_AuthenticateUser_Returns_Failed_When_Password_Is_Invalid()
        {
            // arrange
            var userAccount = TestFactory.User(1);
            mockUserService.Setup(m => m.GetByEmail(userAccount.EmailAddress)).Returns(userAccount);

            // act
            var result = testService.AuthenticateUser(userAccount.EmailAddress, userAccount.Password);

            // assert
            Assert.That(result, Is.EqualTo(AuthenticationResult.Failed));
        }

        [Test]
        public void Test_That_AuthenticateUser_Returns_Success_When_Credentials_Are_Valid()
        {
            // arrange
            var userAccount = TestFactory.User(1);
            var password = userAccount.Password;
            userAccount.Password = "HcOxK3xhFPRKufqn5wwpN4YdBcdmddF8e//s4nguOw2SqvL0omuyik56JAA6EXBD";
            mockUserService.Setup(m => m.GetByEmail(userAccount.EmailAddress)).Returns(userAccount);

            // act
            var result = testService.AuthenticateUser(userAccount.EmailAddress, password);

            // assert
            Assert.That(result, Is.EqualTo(AuthenticationResult.Success));
        }
    }
}
