using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Concrete;

namespace WeeWorld.ADS.Tests.Services
{
    [TestFixture]
    public class GroupServiceTests
    {
        private Mock<IRepository<Group>> mockRepo { get; set; }
        private GroupService testService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IRepository<Group>>();
            testService = new GroupService(mockRepo.Object);
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
            var group = TestFactory.Group(1);
            mockRepo.Setup(m => m.Single(group.Id)).Returns(group);

            // Act
            var response = testService.GetById(group.Id);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(group, response);
            mockRepo.Verify(m => m.Single(group.Id));
        }

        [Test]
        public void Test_That_GetAll_Returns_Group_List()
        {
            // Arrange
            var groups = new List<Group>{
                TestFactory.Group(1),
                TestFactory.Group(2),
                TestFactory.Group(3)
            };
            mockRepo.Setup(m => m.All()).Returns(groups);

            // Act
            var response = testService.GetAll();

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(groups, response);
            mockRepo.Verify(m => m.All());
        }

        [Test]
        public void Test_That_GetByName_Returns_Null_For_Unknown_Name()
        {
            // Arrange

            // Act
            var response = testService.GetByName("TestGroup");

            // Assert
            Assert.Null(response);
        }
        #endregion

        #region create tests
        [Test]
        public void Test_That_Create_Throws_Exception_When_Group_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Group cannot be null"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Name_Is_Null()
        {
            // Arrange
            var newGroup = TestFactory.Group(0);
            newGroup.Name = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newGroup));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name is required"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Name_Already_Exists()
        {
            // Arrange
            var newGroup = TestFactory.Group(0);
            var existingGroup = TestFactory.Group(1);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<Group, bool>>())).Returns(existingGroup);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newGroup));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name already exists"));
        }

        #endregion

        #region update tests
        [Test]
        public void Test_That_Update_Throws_Exception_When_Group_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Group cannot be null"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_EmailAddress_Is_Null()
        {
            // Arrange
            var updatedGroup = TestFactory.Group(0);
            updatedGroup.Name = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedGroup));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name is required"));
        }


        [Test]
        public void Test_That_Update_Throws_Exception_When_Attempting_To_Change_Name_Of_Admin_User()
        {
            // Arrange
            var updatedGroup = TestFactory.Group(1);
            updatedGroup.Name = "Administratorsss";

            var existingAdminGrp = TestFactory.Group(1);
            existingAdminGrp.Name = "Administrators";

            mockRepo.Setup(r => r.Single(updatedGroup.Id)).Returns(existingAdminGrp);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedGroup));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("You cannot change the name of this group"));
        }
        
        [Test]
        public void Test_That_Update_Throws_Exception_When_Nane_Already_Exists()
        {
            // Arrange
            var updatedGroup = TestFactory.Group(1);
            var existingGroup = TestFactory.Group(2);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<Group, bool>>())).Returns(existingGroup);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedGroup));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name already exists"));
        }

        #endregion

        #region delete tests
        [Test]
        public void Test_That_Delete_Throws_Exception_When_Trying_To_Delete_Admin_Group()
        {
            // Arrange
            var group = TestFactory.Group();
            group.Name = "Administrators";

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Delete(group));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("You cannot delete this group"));
        }

        #endregion
    }
}
