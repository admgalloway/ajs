using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Concrete;
using Moq;
using NUnit.Framework;

namespace WeeWorld.ADS.Tests.Services
{
    [TestFixture]
    public class TokenServiceTests
    {
        private Mock<IRepository<Token>> mockRepo { get; set; }
        private TokenService testService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IRepository<Token>>();
            testService = new TokenService(mockRepo.Object);
        }

        #region get tests

        [Test]
        public void Test_That_GetByCode_Returns_Token_For_Known_Code()
        {
            // Arrange
            var token = TestFactory.Token(1);
            mockRepo.Setup(m => m.SingleOrDefault(It.IsAny<Func<Token,bool>>())).Returns(token);

            // Act
            var response = testService.GetByCode(token.Code);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(token, response);
        }

        [Test]
        public void Test_That_GetByUser_Returns_Tokens_For_Known_User()
        {
            // Arrange
            var user = TestFactory.User(1);
            var tokens = new List<Token>{
                TestFactory.Token(1),
                TestFactory.Token(2),
                TestFactory.Token(3)
            };

            mockRepo.Setup(m => m.List(It.IsAny<Func<Token, bool>>())).Returns(tokens);

            // Act
            var response = testService.GetByUser(user.Id);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(3, response.Count);
            Assert.AreEqual(response[0].Expired, false);
            Assert.AreEqual(response[1].Expired, false);
            Assert.AreEqual(response[2].Expired, false);
        }

        [Test]
        public void Test_That_GetByEmail_Returns_Tokens_For_Known_User()
        {
            // Arrange
            var user = TestFactory.User(1);
            var tokens = new List<Token>{
                TestFactory.Token(1),
                TestFactory.Token(2),
                TestFactory.Token(3)
            };

            mockRepo.Setup(m => m.List(It.IsAny<Func<Token, bool>>())).Returns(tokens);

            // Act
            var response = testService.GetByEmail(user.EmailAddress);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Count, 3);
            Assert.AreEqual(response[0].Expired, false);
            Assert.AreEqual(response[1].Expired, false);
            Assert.AreEqual(response[2].Expired, false);
        }

        [Test]
        public void Test_That_IsCodeValid_Returns_False_For_Null_Code()
        {
            // Arrange

            // Act
            var response = testService.IsCodeValid(null, false);

            // Assert
            Assert.AreEqual(response, false);

        }

        [Test]
        public void Test_That_IsCodeValid_Returns_False_For_Unknown_Code()
        {
            // Arrange

            // Act
            var response = testService.IsCodeValid("abcde", false);

            // Assert
            Assert.AreEqual(response, false);

        }

        [Test]
        public void Test_That_IsCodeValid_Returns_False_For_Expired_Code()
        {
            // Arrange
            var token = TestFactory.Token(1);
            var user = TestFactory.User(1);
            token.User = user;
            token.ExpiryDate = DateTime.Now.AddHours(-1);
            mockRepo.Setup(m => m.SingleOrDefault(It.IsAny<Func<Token, bool>>())).Returns(token);

            // Act
            var response = testService.IsCodeValid("abcde", false);

            // Assert
            Assert.AreEqual(response, false);

        }

        [Test]
        public void Test_That_IsCodeValid_Returns_False_For_Valid_Code_But_Requiring_Admin_Rights()
        {
            // Arrange
            var token = TestFactory.Token(1);
            var user = TestFactory.User(1);
            token.User = user;
            mockRepo.Setup(m => m.SingleOrDefault(It.IsAny<Func<Token, bool>>())).Returns(token);

            // Act
            var response = testService.IsCodeValid("abcde", true);

            // Assert
            Assert.AreEqual(response, false);
        }

        [Test]
        public void Test_That_IsCodeValid_Returns_True_For_Valid_Code_Without_Admin_Rights()
        {
            // Arrange
            var token = TestFactory.Token(1);
            var user = TestFactory.User(1);
            token.User = user;
            mockRepo.Setup(m => m.SingleOrDefault(It.IsAny<Func<Token, bool>>())).Returns(token);

            // Act
            var response = testService.IsCodeValid("abcde", false);

            // Assert
            Assert.AreEqual(response, true);
        }

        [Test]
        public void Test_That_IsCodeValid_Returns_True_For_Valid_Code_With_Admin_Rights()
        {
            // Arrange
            var token = TestFactory.Token(1);
            var user = TestFactory.User(1);
            var adminGroup = TestFactory.AdminGroup();
            user.Groups.Add(adminGroup);
            token.User = user;
            mockRepo.Setup(m => m.SingleOrDefault(It.IsAny<Func<Token, bool>>())).Returns(token);

            // Act
            var response = testService.IsCodeValid("abcde", true);

            // Assert
            Assert.AreEqual(response, true);
        }
        
        #endregion

    }
}
