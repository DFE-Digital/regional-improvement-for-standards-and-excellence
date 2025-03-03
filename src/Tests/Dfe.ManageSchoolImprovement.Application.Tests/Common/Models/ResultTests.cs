using Dfe.ManageSchoolImprovement.Application.Common.Models;

namespace Dfe.ManageSchoolImprovement.Application.Tests.Common.Models
{
    public class ResultTests
    {
        [Fact]
        public void Success_Should_Create_Successful_Result()
        {
            // Arrange
            var expectedValue = 42;

            // Act
            var result = Result<int>.Success(expectedValue);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public void Failure_Should_Create_Failed_Result()
        {
            // Arrange
            var expectedError = "An error occurred";

            // Act
            var result = Result<int>.Failure(expectedError);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.Error);
        }

        [Fact]
        public void Success_Should_Create_Successful_Result_With_Reference_Type()
        {
            // Arrange
            var expectedValue = "Hello, World!";

            // Act
            var result = Result<string>.Success(expectedValue);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public void Failure_Should_Create_Failed_Result_With_Reference_Type()
        {
            // Arrange
            var expectedError = "An error occurred";

            // Act
            var result = Result<string>.Failure(expectedError);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.Error);
            Assert.Null(result.Value);
        }
    }
}
