using Dfe.ManageSchoolImprovement.Application.Factories;

namespace Dfe.ManageSchoolImprovement.Application.Tests.Factories
{
    using Xunit;
    using System.Collections.Generic;

    public class PagingResponseFactoryTests
    {
        [Fact]
        public void Create_Should_Return_Correct_PagingResponse_When_No_Next_Page()
        {
            // Arrange
            var path = "/api/test";
            int page = 2;
            int count = 10;
            int recordCount = 20;
            var routeValues = new Dictionary<string, object?>();

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page, result.Page);
            Assert.Equal(recordCount, result.RecordCount);
            Assert.Null(result.NextPageUrl);
        }

        [Fact]
        public void Create_Should_Return_Correct_NextPageUrl_Without_RouteValues()
        {
            // Arrange
            var path = "/api/test";
            int page = 1;
            int count = 10;
            int recordCount = 30;
            var routeValues = new Dictionary<string, object?>();

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page, result.Page);
            Assert.Equal(recordCount, result.RecordCount);
            Assert.Equal("/api/test?page=2&count=10", result.NextPageUrl);
        }

        [Fact]
        public void Create_Should_Return_Correct_NextPageUrl_With_RouteValues()
        {
            // Arrange
            var path = "/api/test";
            int page = 1;
            int count = 10;
            int recordCount = 30;
            var routeValues = new Dictionary<string, object?>
        {
            { "filter", "active" },
            { "sort", "asc" }
        };

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page, result.Page);
            Assert.Equal(recordCount, result.RecordCount);
            Assert.Equal("/api/test?page=2&count=10&filter=active&sort=asc", result.NextPageUrl);
        }

        [Fact]
        public void Create_Should_Ignore_Null_Or_Whitespace_RouteValues()
        {
            // Arrange
            var path = "/api/test";
            int page = 1;
            int count = 10;
            int recordCount = 30;
            var routeValues = new Dictionary<string, object?>
        {
            { "filter", "active" },
            { "empty", "" },
            { "nullValue", null }
        };

            // Act
            var result = PagingResponseFactory.Create(path, page, count, recordCount, routeValues);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page, result.Page);
            Assert.Equal(recordCount, result.RecordCount);
            Assert.Equal("/api/test?page=2&count=10&filter=active", result.NextPageUrl);
        }
    }
}
