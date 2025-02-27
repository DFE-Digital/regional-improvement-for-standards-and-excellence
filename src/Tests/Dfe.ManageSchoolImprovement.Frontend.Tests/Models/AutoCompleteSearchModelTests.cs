using Dfe.ManageSchoolImprovement.Frontend.Models; 

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class AutoCompleteSearchModelTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly_WhenValidParametersProvided()
        {
            // Arrange
            var label = "Search Label";
            var searchQuery = "apple";
            var searchEndpoint = "/api/search";

            // Act
            var autoCompleteSearchModel = new AutoCompleteSearchModel(label, searchQuery, searchEndpoint);

            // Assert
            Assert.Equal(label, autoCompleteSearchModel.Label);
            Assert.Equal(searchQuery, autoCompleteSearchModel.SearchQuery);
            Assert.Equal(searchEndpoint, autoCompleteSearchModel.SearchEndpoint);
        }

        [Fact]
        public void SearchQuery_EscapesSingleQuotes_Correctly()
        {
            // Arrange
            var label = "Search Label";
            var searchQueryWithSingleQuote = "apple's";
            var searchEndpoint = "/api/search";

            // Act
            var autoCompleteSearchModel = new AutoCompleteSearchModel(label, searchQueryWithSingleQuote, searchEndpoint);

            // Assert
            Assert.Equal("apple\\'s", autoCompleteSearchModel.SearchQuery);
        }
    }
}
