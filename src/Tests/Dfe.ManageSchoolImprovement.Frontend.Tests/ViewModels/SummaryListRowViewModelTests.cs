using Dfe.ManageSchoolImprovement.Frontend.ViewModels;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.ViewModels
{
    public class SummaryListRowViewModelTests
    {
        [Fact]
        public void SummaryListRowViewModel_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var model = new SummaryListRowViewModel();

            // Assert
            Assert.Null(model.Id);
            Assert.Null(model.Key);
            Assert.Null(model.Value);
            Assert.Null(model.ValueLink);
            Assert.Null(model.AdditionalText);
            Assert.False(model.HasValue);
            Assert.False(model.HasAdditionalText);
            Assert.False(model.HasValueLink);
            Assert.Null(model.Page);
            Assert.Null(model.Fragment);
            Assert.Null(model.RouteId);
            Assert.Null(model.Return);
            Assert.Null(model.HiddenText);
            Assert.Null(model.KeyWidth);
            Assert.Null(model.ValueWidth);
            Assert.Null(model.Name);
            Assert.False(model.HighlightNegativeValue);
            Assert.False(model.IsReadOnly);
            Assert.Equal(string.Empty, model.NegativeStyleClass);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldSetAndGetValues()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Id = "row1",
                Key = "Key1",
                Value = "£100.00",
                ValueLink = "https://example.com",
                AdditionalText = "Some additional text",
                Page = "page1",
                Fragment = "fragment1",
                RouteId = "route1",
                Return = "return1",
                HiddenText = "hidden info",
                KeyWidth = "200px",
                ValueWidth = "100px",
                Name = "name1",
                HighlightNegativeValue = true,
                IsReadOnly = false
            };

            // Act & Assert
            Assert.Equal("row1", model.Id);
            Assert.Equal("Key1", model.Key);
            Assert.Equal("£100.00", model.Value);
            Assert.Equal("https://example.com", model.ValueLink);
            Assert.Equal("Some additional text", model.AdditionalText);
            Assert.Equal("page1", model.Page);
            Assert.Equal("fragment1", model.Fragment);
            Assert.Equal("route1", model.RouteId);
            Assert.Equal("return1", model.Return);
            Assert.Equal("hidden info", model.HiddenText);
            Assert.Equal("200px", model.KeyWidth);
            Assert.Equal("100px", model.ValueWidth);
            Assert.Equal("name1", model.Name);
            Assert.True(model.HighlightNegativeValue);
            Assert.False(model.IsReadOnly);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnTrueForHasValue_WhenValueIsNotEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = "Some value"
            };

            // Act & Assert
            Assert.True(model.HasValue);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnFalseForHasValue_WhenValueIsEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = string.Empty
            };

            // Act & Assert
            Assert.False(model.HasValue);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnTrueForHasAdditionalText_WhenAdditionalTextIsNotEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                AdditionalText = "Additional Info"
            };

            // Act & Assert
            Assert.True(model.HasAdditionalText);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnFalseForHasAdditionalText_WhenAdditionalTextIsEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                AdditionalText = string.Empty
            };

            // Act & Assert
            Assert.False(model.HasAdditionalText);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnTrueForHasValueLink_WhenValueLinkIsNotEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                ValueLink = "https://example.com"
            };

            // Act & Assert
            Assert.True(model.HasValueLink);
        }

        [Fact]
        public void SummaryListRowViewModel_ShouldReturnFalseForHasValueLink_WhenValueLinkIsEmpty()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                ValueLink = string.Empty
            };

            // Act & Assert
            Assert.False(model.HasValueLink);
        }

        [Fact]
        public void SummaryListRowViewModel_NegativeStyleClass_ShouldReturnEmpty_WhenValueIsNotNegative()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = "£100.00",
                HighlightNegativeValue = true
            };

            // Act & Assert
            Assert.Equal(string.Empty, model.NegativeStyleClass);
        }

        [Fact]
        public void SummaryListRowViewModel_NegativeStyleClass_ShouldReturnNegativeClass_WhenValueIsNegativeAndHighlightNegativeValueIsTrue()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = "£-100.00",
                HighlightNegativeValue = true
            };

            // Act & Assert
            Assert.Equal("negative-value", model.NegativeStyleClass);
        }

        [Fact]
        public void SummaryListRowViewModel_NegativeStyleClass_ShouldReturnEmpty_WhenValueIsNegativeButHighlightNegativeValueIsFalse()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = "£-100.00",
                HighlightNegativeValue = false
            };

            // Act & Assert
            Assert.Equal(string.Empty, model.NegativeStyleClass);
        }

        [Fact]
        public void SummaryListRowViewModel_NegativeStyleClass_ShouldReturnEmpty_WhenValueIsNotParsableToDecimal()
        {
            // Arrange
            var model = new SummaryListRowViewModel
            {
                Value = "Not a number",
                HighlightNegativeValue = true
            };

            // Act & Assert
            Assert.Equal(string.Empty, model.NegativeStyleClass);
        }
    }
}
