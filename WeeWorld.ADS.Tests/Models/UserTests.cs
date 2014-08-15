using System.Linq;
using NUnit.Framework;

namespace WeeWorld.ADS.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Test_That_Applications_Returns_A_Distinct_List()
        {
            /// Arrange
            var app1 = TestFactory.App(1);
            var app2 = TestFactory.App(2);
            var app3 = TestFactory.App(3);
            var app4 = TestFactory.App(4);

            var grp1 = TestFactory.Group(1);
            var grp2 = TestFactory.Group(2);
            var grp3 = TestFactory.Group(3);

            grp1.Applications.Add(app1);
            grp1.Applications.Add(app2);
            grp1.Applications.Add(app3);

            grp2.Applications.Add(app1);
            grp2.Applications.Add(app2);

            grp1.Applications.Add(app2);
            grp1.Applications.Add(app3);

            // Act
            var user = TestFactory.User(1);
            user.Groups.Add(grp1);
            user.Groups.Add(grp2);
            user.Groups.Add(grp3);

            // Assert
            Assert.NotNull(user.Applications);
            Assert.AreEqual(3, user.Applications.Count());
        }

        [Test]
        public void Test_That_IsAdmin_Returns_True_If_User_In_Admin_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);
            user.Groups.Add(TestFactory.AdminGroup());

            // Assert
            Assert.True(user.IsAdmin);
        }

        [Test]
        public void Test_That_IsAdmin_Returns_False_If_User_Not_In_Admin_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);

            // Assert
            Assert.False(user.IsAdmin);
        }

        [Test]
        public void Test_That_IsInGroup_Returns_True_If_User_In_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);
            var grp = TestFactory.Group(1);
            user.Groups.Add(grp);

            // Assert
            Assert.True(user.IsInGroup(grp.Name));
            Assert.True(user.IsInGroup(grp.Name.ToLower()));
            Assert.True(user.IsInGroup(grp.Name.ToUpper()));
        }

        [Test]
        public void Test_That_IsAdmin_Returns_False_If_User_Not_In_Group()
        {
            // Arrange (Act)
            var user = TestFactory.User(1);
            var grp = TestFactory.Group(1);

            // Assert
            Assert.False(user.IsInGroup(grp.Name));
            Assert.False(user.IsInGroup(grp.Name.ToLower()));
            Assert.False(user.IsInGroup(grp.Name.ToUpper()));
        }

    }
}
