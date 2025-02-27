using Dfe.ManageSchoolImprovement.Application.Common.Models;
namespace Dfe.ManageSchoolImprovement.Application.Tests.Common.Models
{
    public class PersonTests
    {
        [Fact]
        public void Person_Should_InitializePropertiesCorrectly()
        {
            // Arrange
            var roles = new List<string> { "Admin", "User" };
            var updatedAt = DateTime.UtcNow;

            // Act
            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DisplayName = "John Doe",
                DisplayNameWithTitle = "Mr. John Doe",
                Phone = "123-456-7890",
                Roles = roles,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(1, person.Id);
            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
            Assert.Equal("john.doe@example.com", person.Email);
            Assert.Equal("John Doe", person.DisplayName);
            Assert.Equal("Mr. John Doe", person.DisplayNameWithTitle);
            Assert.Equal("123-456-7890", person.Phone);
            Assert.Equal(roles, person.Roles);
            Assert.Equal(updatedAt, person.UpdatedAt);
        }
    }
}
