using System.Linq;
using NUnit.Framework;

namespace WeeWorld.ADS.Tests.Models
{
    [TestFixture]
    public class ApplicationTests
    {
        [Test]
        public void Test_That_Users_Returns_A_Distinct_List()
        {
            // Arrange
            var user1 = TestFactory.User(1);
            var user2 = TestFactory.User(2);
            var user3 = TestFactory.User(3);
            var user4 = TestFactory.User(4);

            var grp1 = TestFactory.Group(1);
            var grp2 = TestFactory.Group(2);
            var grp3 = TestFactory.Group(3);

            grp1.Users.Add(user1);
            grp1.Users.Add(user2);
            grp1.Users.Add(user3);
            
            grp2.Users.Add(user1);
            grp2.Users.Add(user2);
            
            grp1.Users.Add(user2);
            grp1.Users.Add(user3);

            // Act
            var app = TestFactory.App(1);
            app.Groups.Add(grp1);
            app.Groups.Add(grp2);
            app.Groups.Add(grp3);

            // Assert
            Assert.NotNull(app.Users);
            Assert.AreEqual(3, app.Users.Count());
        }
    }
}
