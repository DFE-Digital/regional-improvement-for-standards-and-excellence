using Dfe.ManageSchoolImprovement.Frontend.ViewModels;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.ViewModels
{
    public class TextInputViewModelTests
    {
        [Fact]
        public void TextInputViewModel_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var model = new TextInputViewModel();

            // Assert
            Assert.Null(model.Id);
            Assert.Null(model.Name);
            Assert.Null(model.Value);
            Assert.Null(model.Label); // Label can be null initially
            Assert.Null(model.ErrorMessage);
            Assert.Equal(0, model.Width);
            Assert.Null(model.Hint);
            Assert.False(model.HeadingLabel);
        }

        [Fact]
        public void TextInputViewModel_ShouldSetAndGetValues()
        {
            // Arrange
            var model = new TextInputViewModel
            {
                Id = "input1",
                Name = "username",
                Value = "john_doe",
                Label = "Username",
                ErrorMessage = "Username is required",
                Width = 200,
                Hint = "Enter your username",
                HeadingLabel = true
            };

            // Act & Assert
            Assert.Equal("input1", model.Id);
            Assert.Equal("username", model.Name);
            Assert.Equal("john_doe", model.Value);
            Assert.Equal("Username", model.Label);
            Assert.Equal("Username is required", model.ErrorMessage);
            Assert.Equal(200, model.Width);
            Assert.Equal("Enter your username", model.Hint);
            Assert.True(model.HeadingLabel);
        }
    }
}
