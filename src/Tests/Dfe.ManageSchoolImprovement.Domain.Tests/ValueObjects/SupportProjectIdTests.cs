using Dfe.ManageSchoolImprovement.Domain.ValueObjects;

namespace Dfe.ManageSchoolImprovement.Domain.Tests.ValueObjects
{
    public class SupportProjectIdTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            int expectedValue = 123;

            // Act
            var supportProjectId = new SupportProjectId(expectedValue);

            // Assert
            Assert.Equal(expectedValue, supportProjectId.Value);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            int value = 123;
            var supportProjectId = new SupportProjectId(value);

            // Act
            string result = supportProjectId.ToString();

            // Assert
            Assert.Equal($"SupportProjectId {{ Value = {value} }}", result);
        }
    }
}
