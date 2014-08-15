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
    public class UserServiceTests
    {
        private Mock<IRepository<User>> mockRepo { get; set; }
        private UserService testService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IRepository<User>>();
            testService = new UserService(mockRepo.Object);
        }

        #region get tests
        [Test]
        public void Test_That_GetById_Returns_Null_For_Invalid_Id()
        {
            // Arrange

            // Act
            var response = testService.GetById(0);

            // Assert
            Assert.Null(response);
        }

        [Test]
        public void Test_That_GetById_Returns_Null_For_Unknown_Id()
        {
            // Arrange

            // Act
            var response = testService.GetById(25);

            // Assert
            Assert.Null(response);
            mockRepo.Verify(m => m.Single(25));
        }

        [Test]
        public void Test_That_GetById_Returns_User_For_Known_Id()
        {
            // Arrange
            var user = TestFactory.User(1);
            mockRepo.Setup(m => m.Single(user.Id)).Returns(user);

            // Act
            var response = testService.GetById(user.Id);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(user, response);
            mockRepo.Verify(m => m.Single(user.Id));
        }

        [Test]
        public void Test_That_GetAll_Returns_User_List()
        {
            // Arrange
            var users = new List<User>{
                TestFactory.User(1),
                TestFactory.User(2),
                TestFactory.User(3)
            };
            mockRepo.Setup(m => m.All()).Returns(users);

            // Act
            var response = testService.GetAll();

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(users, response);
            mockRepo.Verify(m => m.All());
        }

        [Test]
        public void Test_That_GetByEmail_Returns_Null_For_Unknown_EmailAddress()
        {
            // Arrange

            // Act
            var response = testService.GetByEmail("anything@weeworld.com");

            // Assert
            Assert.Null(response);
        }
        #endregion

        #region create tests
        [Test]
        public void Test_That_Create_Throws_Exception_When_User_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("User cannot be null"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_EmailAddress_Is_Null()
        {
            // Arrange
            var newUser = TestFactory.User(0);
            newUser.EmailAddress = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address is required"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_EmailAddress_Is_In_An_Invalid_Format()
        {
            // Arrange
            var newUser = TestFactory.User(0);
            newUser.EmailAddress = "hello";

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address format is invalid"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_EmailAddress_Already_Exists()
        {
            // Arrange
            var newUser = TestFactory.User(0);
            var existingUser = TestFactory.User(1);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address already exists"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Password_Is_Null()
        {
            // Arrange
            var newUser = TestFactory.User(0);
            newUser.Password = null;
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<User, bool>>())).Returns(newUser);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Password"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Password is required"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Password_Is_Invalid()
        {
            // Arrange
            var newUser = TestFactory.User(0);
            newUser.Password = "abc";
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<User, bool>>())).Returns(newUser);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Password"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Password must be at least 4 characters"));
        }
        #endregion

        #region update tests
        [Test]
        public void Test_That_Update_Throws_Exception_When_User_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("User cannot be null"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_EmailAddress_Is_Null()
        {
            // Arrange
            var updatedUser = TestFactory.User(0);
            updatedUser.EmailAddress = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address is required"));
        }


        [Test]
        public void Test_That_Update_Throws_Exception_When_Attempting_To_Change_EmailAddress_Of_Admin_User()
        {
            // Arrange
            var updatedUser = TestFactory.User(1);
            updatedUser.EmailAddress = "madmin@weeworld.com";

            var existingAdmin = TestFactory.User(1);
            existingAdmin.EmailAddress = "admin@weeworld.com";

            mockRepo.Setup(r => r.Single(updatedUser.Id)).Returns(existingAdmin);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("You cannot change the email address of this user"));
        }
        
        [Test]
        public void Test_That_Update_Throws_Exception_When_EmailAddress_Is_In_An_Invalid_Format()
        {
            // Arrange
            var updatedUser = TestFactory.User(0);
            updatedUser.EmailAddress = "hello";

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address format is invalid"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_EmailAddress_Already_Exists()
        {
            // Arrange
            var updatedUser = TestFactory.User(1);
            var existingUser = TestFactory.User(2);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Email Address already exists"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_Password_Is_Invalid()
        {
            // Arrange
            var updatedUser = TestFactory.User(1);
            updatedUser.Password = "abc";
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<User, bool>>())).Returns(updatedUser);

            // Arrange
            var existingAdmin = TestFactory.User(1);
            mockRepo.Setup(r => r.Single(updatedUser.Id)).Returns(existingAdmin);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedUser));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Password"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Password must be at least 4 characters"));
        }
        #endregion

        #region delete tests
        [Test]
        public void Test_That_Delete_Throws_Exception_When_Trying_To_Delete_Default_User()
        {
            // Arrange
            var user = TestFactory.User();
            user.EmailAddress = "admin@weeworld.com";

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Delete(user));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("EmailAddress"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("You cannot delete this account"));
        }


        #endregion

    }
}
