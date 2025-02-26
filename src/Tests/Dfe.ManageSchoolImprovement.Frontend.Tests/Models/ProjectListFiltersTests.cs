
using Dfe.ManageSchoolImprovement.Frontend.Models;
using Microsoft.Extensions.Primitives;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Models
{
    public class ProjectListFiltersTests
    {
        private IDictionary<string, object?> _store;

        public ProjectListFiltersTests()
        {
            _store = new Dictionary<string, object?>();
        }

        [Fact]
        public void Constructor_SetsEmptyValues_WhenInitialized()
        {
            // Arrange & Act
            var projectListFilters = new ProjectListFilters();

            // Assert
            Assert.Empty(projectListFilters.AvailableStatuses);
            Assert.Empty(projectListFilters.AvailableDeliveryOfficers);
            Assert.Empty(projectListFilters.AvailableRegions);
            Assert.Empty(projectListFilters.AvailableLocalAuthorities);
            Assert.Empty(projectListFilters.AvailableAdvisoryBoardDates);
            Assert.Null(projectListFilters.Title);
            Assert.Empty(projectListFilters.SelectedStatuses);
            Assert.Empty(projectListFilters.SelectedOfficers);
            Assert.Empty(projectListFilters.SelectedRegions);
            Assert.Empty(projectListFilters.SelectedLocalAuthorities);
            Assert.Empty(projectListFilters.SelectedAdvisoryBoardDates);
        }

        [Fact]
        public void PersistUsing_CachesFilterValues()
        {
            // Arrange
            var projectListFilters = new ProjectListFilters();
            var store = new Dictionary<string, object?>
            {
                { ProjectListFilters.FilterTitle,  new string[]{"Test Title" } },
                { ProjectListFilters.FilterStatuses, new string[] { "Status1", "Status2" } }
            };

            // Act
            projectListFilters.PersistUsing(store);

            // Assert
            Assert.Equal("Test Title", projectListFilters.Title);
            Assert.Equal(new string[] { "Status1", "Status2" }, projectListFilters.SelectedStatuses);
        }

        [Fact]
        public void IsVisible_ReturnsTrue_WhenAnySelectedFiltersExist()
        {
            // Arrange
            var projectListFilters = new ProjectListFilters
            {
                SelectedStatuses = ["Status1"]
            };

            // Act
            var isVisible = projectListFilters.IsVisible;

            // Assert
            Assert.True(isVisible);
        }

        [Fact]
        public void IsVisible_ReturnsFalse_WhenNoSelectedFiltersExist()
        {
            // Arrange
            var projectListFilters = new ProjectListFilters();

            // Act
            var isVisible = projectListFilters.IsVisible;

            // Assert
            Assert.False(isVisible);
        }

        [Fact]
        public void PopulateFrom_ClearsFilters_WhenQueryStringContainsClearKey()
        {
            // Arrange
            var query = new Dictionary<string, StringValues>
            {
                { "clear", "true" }
            };

            var projectListFilters = new ProjectListFilters();
            projectListFilters.PersistUsing(new Dictionary<string, object?>
            {
                { ProjectListFilters.FilterStatuses, new string[] { "Status1", "Status2" } }
            });

            // Act
            projectListFilters.PopulateFrom(query);

            // Assert
            Assert.Null(projectListFilters.Title);
            Assert.Empty(projectListFilters.SelectedStatuses);
            Assert.Empty(projectListFilters.SelectedOfficers);
            Assert.Empty(projectListFilters.SelectedRegions);
            Assert.Empty(projectListFilters.SelectedLocalAuthorities);
            Assert.Empty(projectListFilters.SelectedAdvisoryBoardDates);
        }

        [Fact]
        public void PopulateFrom_RemovesFilters_WhenQueryStringContainsRemoveKey()
        {
            // Arrange
            var query = new Dictionary<string, StringValues>
            {
                { "remove", "true" },
                { "SelectedStatuses", new StringValues(new[] { "Status1" }) }
            };

            var store = new Dictionary<string, object?>
            {
                { ProjectListFilters.FilterStatuses, new string[] { "Status1", "Status2" } }
            };
            var projectListFilters = new ProjectListFilters();
            projectListFilters.PersistUsing(store);

            // Act
            projectListFilters.PopulateFrom(query);

            // Assert
            Assert.Equal(new string[] { "Status2" }, projectListFilters.SelectedStatuses);
        }
    }
}
