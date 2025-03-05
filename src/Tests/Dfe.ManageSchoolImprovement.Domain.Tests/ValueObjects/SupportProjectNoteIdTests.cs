using Dfe.ManageSchoolImprovement.Domain.ValueObjects; 

namespace Dfe.ManageSchoolImprovement.Domain.Tests.ValueObjects
{
    public class SupportProjectNoteIdTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedValue = Guid.NewGuid();

            // Act
            var supportProjectNoteId = new SupportProjectNoteId(expectedValue);

            // Assert
            Assert.Equal(expectedValue, supportProjectNoteId.Value);
        }
        
        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            Guid value = Guid.NewGuid();
            var supportProjectNoteId = new SupportProjectNoteId(value);

            // Act
            string result = supportProjectNoteId.ToString();

            // Assert
            Assert.Equal($"SupportProjectNoteId {{ Value = {value} }}", result);
        }
    }
}
