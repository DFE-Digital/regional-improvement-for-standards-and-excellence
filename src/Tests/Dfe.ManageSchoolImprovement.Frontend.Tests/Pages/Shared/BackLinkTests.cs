using Dfe.ManageSchoolImprovement.Frontend.Pages.Shared;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Pages.Shared
{
    public class BackLinkTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly_WhenParametersProvided()
        {
            // Arrange
            var linkPage = "HomePage";
            var linkRouteValues = new Dictionary<string, string>
            {
                { "id", "123" },
                { "name", "JohnDoe" }
            };
            var linkText = "Go Back";

            // Act
            var backLink = new BackLink(linkPage, linkRouteValues, linkText);

            // Assert
            Assert.Equal(linkPage, backLink.LinkPage);
            Assert.Equal(linkRouteValues, backLink.LinkRouteValues);
            Assert.Equal(linkText, backLink.LinkText);
        }

        [Fact]
        public void Constructor_SetsDefaultLinkText_WhenNoLinkTextProvided()
        {
            // Arrange
            var linkPage = "HomePage";
            var linkRouteValues = new Dictionary<string, string>
            {
                { "id", "123" }
            };

            // Act
            var backLink = new BackLink(linkPage, linkRouteValues);

            // Assert
            Assert.Equal("Back", backLink.LinkText);
        }

        [Fact]
        public void Constructor_SetsLinkPageCorrectly()
        {
            // Arrange
            var linkPage = "AboutPage";
            var linkRouteValues = new Dictionary<string, string>
            {
                { "id", "456" }
            };
            var linkText = "Go Back";

            // Act
            var backLink = new BackLink(linkPage, linkRouteValues, linkText);

            // Assert
            Assert.Equal(linkPage, backLink.LinkPage);
        }

        [Fact]
        public void Constructor_SetsLinkRouteValuesCorrectly()
        {
            // Arrange
            var linkPage = "ContactPage";
            var linkRouteValues = new Dictionary<string, string>
            {
                { "category", "support" },
                { "item", "service" }
            };
            var linkText = "Back to Contact";

            // Act
            var backLink = new BackLink(linkPage, linkRouteValues, linkText);

            // Assert
            Assert.Equal(linkRouteValues, backLink.LinkRouteValues);
        }

        [Fact]
        public void LinkPageProperty_CanBeSetAndGet()
        {
            // Arrange
            var linkPage = "HomePage";
            var linkRouteValues = new Dictionary<string, string> { { "id", "123" } };

            var backLink = new BackLink(linkPage, linkRouteValues);

            // Act
            backLink.LinkPage = "NewPage";

            // Assert
            Assert.Equal("NewPage", backLink.LinkPage);
        }

        [Fact]
        public void LinkRouteValuesProperty_CanBeSetAndGet()
        {
            // Arrange
            var linkPage = "ProductsPage";
            var linkRouteValues = new Dictionary<string, string> { { "category", "electronics" } };

            var backLink = new BackLink(linkPage, linkRouteValues)
            {
                // Act
                LinkRouteValues = new Dictionary<string, string> { { "category", "clothing" } }
            };

            // Assert
            Assert.Contains(new KeyValuePair<string, string>("category", "clothing"), backLink.LinkRouteValues);
        }

        [Fact]
        public void LinkTextProperty_CanBeSetAndGet()
        {
            // Arrange
            var linkPage = "ProfilePage";
            var linkRouteValues = new Dictionary<string, string> { { "userId", "1" } };

            var backLink = new BackLink(linkPage, linkRouteValues);

            // Act
            backLink.LinkText = "Return to Profile";

            // Assert
            Assert.Equal("Return to Profile", backLink.LinkText);
        }
    }
}
