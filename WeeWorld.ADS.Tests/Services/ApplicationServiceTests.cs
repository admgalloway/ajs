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
    public class ApplicationServiceTests
    {
        private Mock<IRepository<Application>> mockRepo { get; set; }
        private ApplicationService testService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IRepository<Application>>();
            testService = new ApplicationService(mockRepo.Object);
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
        public void Test_That_GetById_Returns_Application_For_Known_Id()
        {
            // Arrange
            var app = TestFactory.App(1);
            mockRepo.Setup(m => m.Single(app.Id)).Returns(app);

            // Act
            var response = testService.GetById(app.Id);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(app, response);
            mockRepo.Verify(m => m.Single(app.Id));
        }

        [Test]
        public void Test_That_GetAll_Returns_App_List()
        {
            // Arrange
            var apps = new List<Application>{
                TestFactory.App(1),
                TestFactory.App(2),
                TestFactory.App(3)
            };
            mockRepo.Setup(m => m.All()).Returns(apps);

            // Act
            var response = testService.GetAll();

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(apps, response);
            mockRepo.Verify(m => m.All());
        }

        [Test]
        public void Test_That_GetByName_Returns_Null_For_Unknown_Name()
        {
            // Arrange

            // Act
            var response = testService.GetByName("TestApplication");

            // Assert
            Assert.Null(response);
        }
        #endregion

        #region create tests
        [Test]
        public void Test_That_Create_Throws_Exception_When_Application_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Application cannot be null"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Name_Is_Null()
        {
            // Arrange
            var newApp = TestFactory.App(0);
            newApp.Name = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newApp));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name is required"));
        }

        [Test]
        public void Test_That_Create_Throws_Exception_When_Name_Already_Exists()
        {
            // Arrange
            var newApp = TestFactory.App(0);
            var existingApp = TestFactory.App(1);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<Application, bool>>())).Returns(existingApp);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Create(newApp));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name already exists"));
        }

        #endregion

        #region update tests
        [Test]
        public void Test_That_Update_Throws_Exception_When_Application_Is_Null()
        {
            // Arrange

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(null));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Object"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Application cannot be null"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_Name_Is_Null()
        {
            // Arrange
            var updatedApp = TestFactory.App(0);
            updatedApp.Name = null;

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedApp));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name is required"));
        }

        [Test]
        public void Test_That_Update_Throws_Exception_When_Name_Already_Exists()
        {
            // Arrange
            var updatedApp = TestFactory.App(1);
            var existingApp = TestFactory.App(2);
            mockRepo.Setup(r => r.SingleOrDefault(It.IsAny<Func<Application, bool>>())).Returns(existingApp);

            // Act
            var response = Assert.Throws<ValidationException>(() => testService.Update(updatedApp));

            // Assert
            Assert.That(response.Message, Is.EqualTo("Validation Error(s). Inspect error list for details"));
            Assert.That(response.Errors.First().Field, Is.EqualTo("Name"));
            Assert.That(response.Errors.First().Details, Is.EqualTo("Name already exists"));
        }

        #endregion

    }
}
