using Dfe.ManageSchoolImprovement.Frontend.Models; 

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly_WhenValidParametersProvided()
        {
            // Arrange
            var id = "12345";
            var emailAddress = "test@example.com";
            var fullName = "John Doe";

            // Act
            var user = new User(id, emailAddress, fullName);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(emailAddress, user.EmailAddress);
            Assert.Equal(fullName, user.FullName);
        }
    }
}
