using Dfe.ManageSchoolImprovement.Application.Common.Models; 

namespace Dfe.ManageSchoolImprovement.Application.Tests.Common.Models
{
    public class PrincipalTests
    {
        [Fact]
        public void Principal_Should_InitializePropertiesCorrectly()
        {
            // Arrange
            var roles = new List<string> { "Admin", "User" };
            var updatedAt = DateTime.UtcNow;
            var schoolName = "School Name";

            // Act
            var principal = new Principal
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DisplayName = "John Doe",
                DisplayNameWithTitle = "Mr. John Doe",
                Phone = "123-456-7890",
                Roles = roles,
                UpdatedAt = updatedAt,
                SchoolName = schoolName
            };

            // Assert
            Assert.Equal(1, principal.Id);
            Assert.Equal("John", principal.FirstName);
            Assert.Equal("Doe", principal.LastName);
            Assert.Equal("john.doe@example.com", principal.Email);
            Assert.Equal("John Doe", principal.DisplayName);
            Assert.Equal("Mr. John Doe", principal.DisplayNameWithTitle);
            Assert.Equal("123-456-7890", principal.Phone);
            Assert.Equal(roles, principal.Roles);
            Assert.Equal(updatedAt, principal.UpdatedAt);
            Assert.Equal(schoolName, principal.SchoolName);
        }
    }
}
