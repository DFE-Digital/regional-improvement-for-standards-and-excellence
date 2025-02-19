using Dfe.ManageSchoolImprovement.Application.Factories;

namespace Dfe.ManageSchoolImprovement.Application.Tests.Factories
{
    public class PagingResponseFactoryTests
    {
        [Fact]
        public void Create_ReturnsCorrectPagingResponse_WhenThereIsNextPage()
        {
            // Arrange
            var path = "/api/items";
            var page = 1;
            var count = 10;
            var recordCount = 25; // Total records (indicating more pages exist)
            var routeValues = new Dictionary<string, object?>();

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.Equal(25, result.RecordCount);
            Assert.Equal(1, result.Page);
            Assert.Equal("/api/items?page=2&count=10", result.NextPageUrl);
        }

        [Fact]
        public void Create_ReturnsPagingResponseWithoutNextPageUrl_WhenLastPage()
        {
            // Arrange
            var path = "/api/items";
            var page = 3;  // Last page for the given count
            var count = 10;
            var recordCount = 30; // Total records
            var routeValues = new Dictionary<string, object?>();

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.Equal(30, result.RecordCount);
            Assert.Equal(3, result.Page);
            Assert.Null(result.NextPageUrl); // No next page when we are on the last page
        }

        [Fact]
        public void Create_ReturnsCorrectNextPageUrl_WithRouteValues()
        {
            // Arrange
            var path = "/api/items";
            var page = 1;
            var count = 10;
            var recordCount = 25;
            var routeValues = new Dictionary<string, object?>
        {
            { "category", "books" },
            { "sort", "asc" }
        };

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.Equal(25, result.RecordCount);
            Assert.Equal(1, result.Page);
            Assert.Equal("/api/items?page=2&count=10&category=books&sort=asc", result.NextPageUrl);
        }

        [Fact]
        public void Create_DoesNotIncludeEmptyRouteValuesInNextPageUrl()
        {
            // Arrange
            var path = "/api/items";
            var page = 1;
            var count = 10;
            var recordCount = 25;
            var routeValues = new Dictionary<string, object?>
        {
            { "category", "books" },
            { "filter", "" } // Empty string, should be ignored
        };

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.Equal(25, result.RecordCount);
            Assert.Equal(1, result.Page);
            Assert.Equal("/api/items?page=2&count=10&category=books", result.NextPageUrl);
        }

        [Fact]
        public void Create_DoesNotIncludeNullRouteValuesInNextPageUrl()
        {
            // Arrange
            var path = "/api/items";
            var page = 1;
            var count = 10;
            var recordCount = 25;
            var routeValues = new Dictionary<string, object?>
        {
            { "category", "books" },
            { "filter", null } // Null value, should be ignored
        };

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.Equal(25, result.RecordCount);
            Assert.Equal(1, result.Page);
            Assert.Equal("/api/items?page=2&count=10&category=books", result.NextPageUrl);
        }
    }
}
