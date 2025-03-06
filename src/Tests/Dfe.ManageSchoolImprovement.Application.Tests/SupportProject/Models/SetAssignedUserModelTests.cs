using Dfe.ManageSchoolImprovement.Application.SupportProject.Models;

namespace Dfe.ManageSchoolImprovement.Application.Tests.SupportProject.Models
{
    public class SetAssignedUserModelTests
    {
        [Fact]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            // Act
            var model = new SetAssignedUserModel();

            // Assert
            Assert.Equal(0, model.Id);
            Assert.Equal(Guid.Empty, model.UserId);
            Assert.Null(model.FullName);
            Assert.Null(model.EmailAddress);
        }

        [Fact]
        public void ParameterizedConstructor_ShouldInitializeProperties()
        {
            // Arrange
            int id = 1;
            var userId = Guid.NewGuid();
            string fullName = "John Doe";
            string emailAddress = "john.doe@example.com";

            // Act
            var model = new SetAssignedUserModel(id, userId, fullName, emailAddress);

            // Assert
            Assert.Equal(id, model.Id);
            Assert.Equal(userId, model.UserId);
            Assert.Equal(fullName, model.FullName);
            Assert.Equal(emailAddress, model.EmailAddress);
        }

        [Fact]
        public void Properties_ShouldGetAndSetValues()
        {
            // Arrange
            var model = new SetAssignedUserModel();
            int id = 1;
            var userId = Guid.NewGuid();
            string fullName = "Jane Doe";
            string emailAddress = "jane.doe@example.com";

            // Act
            model.Id = id;
            model.UserId = userId;
            model.FullName = fullName;
            model.EmailAddress = emailAddress;

            // Assert
            Assert.Equal(id, model.Id);
            Assert.Equal(userId, model.UserId);
            Assert.Equal(fullName, model.FullName);
            Assert.Equal(emailAddress, model.EmailAddress);
        }
    }
}
