using Dfe.ManageSchoolImprovement.Frontend.Models;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class ErrorTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly_WhenValidParametersProvided()
        {
            // Arrange
            var key = "InvalidInput";
            var message = "The input is invalid.";
            var invalidInputs = new List<string> { "field1", "field2" };

            // Act
            var error = new Error
            {
                Key = key,
                Message = message,
                InvalidInputs = invalidInputs
            };

            // Assert
            Assert.Equal(key, error.Key);
            Assert.Equal(message, error.Message);
            Assert.Equal(invalidInputs, error.InvalidInputs);
        }

        [Fact]
        public void InvalidInputs_IsInitializedAsEmptyList_ByDefault()
        {
            // Arrange & Act
            var error = new Error();

            // Assert
            Assert.Empty(error.InvalidInputs);
        }
    }
}
