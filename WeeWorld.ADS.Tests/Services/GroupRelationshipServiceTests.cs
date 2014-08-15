using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class GroupRelationshipServiceTests
    {
        private GroupRelationshipService testService { get; set; }
        private Mock<IRepository<User>> mockUserRepo { get; set; }
        private Mock<IApplicationService> mockAppService { get; set; }
        private Mock<IGroupService> mockGroupService { get; set; }

        [SetUp]
        public void SetUp()
        {
            mockUserRepo = new Mock<IRepository<User>>();
            mockAppService = new Mock<IApplicationService>();
            mockGroupService = new Mock<IGroupService>();

            testService = new GroupRelationshipService(mockUserRepo.Object, mockAppService.Object, mockGroupService.Object);
        }

        [Test]
        public void Test_That_SaveUserGroups_Clears_Groups_When_Null_Provided()
        { 
            // Arrange
            var existingGroup1 = TestFactory.Group(1);
            var existingGroup2 = TestFactory.Group(2);
            var user = TestFactory.User(1);
            user.Groups.Add(existingGroup1);
            user.Groups.Add(existingGroup2);
            // Act
            mockUserRepo.Setup(m => m.Single(user.Id)).Returns(user);
            mockUserRepo.Setup(m => m.Update(user)).Returns(user);

            // Assert
            var response = testService.SaveUserGroups(user.Id, null);
            //var response = testService.SaveUserGroups(user.Id, new[] { existingGroup1.Id, existingGroup2.Id });

            Assert.NotNull(response);
            Assert.NotNull(response.Groups);
            Assert.AreEqual(0, response.Groups.Count);
        }

        [Test]
        public void Test_That_SaveUserGroups_Clears_Groups_When_Empty_List_Provided()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Test_That_SaveUserGroups_Saves_Groups_When_List_Provided()
        {
            // Arrange

            // Act

            // Assert
        }

    }
}
