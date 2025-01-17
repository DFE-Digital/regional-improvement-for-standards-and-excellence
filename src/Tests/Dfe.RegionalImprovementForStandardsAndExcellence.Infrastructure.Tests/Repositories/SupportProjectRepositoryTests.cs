using FluentAssertions;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Tests.Repositories
{
    public class SupportProjectRepositoryTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task SearchForSupportProjects_ShouldReturnFilteredAndPagedResults()
        {
            // Arrange
            var service = new SupportProjectRepository(fixture.Context);

            // Act 
            var (projects, totalCount) = await service.SearchForSupportProjects(
                title: "School",
                states: new List<string> { },
                advisors: new List<string> { "User1" },
                regions: new List<string> { "Region1" },
                localAuthorities: new List<string> { "Authority1" },
                page: 1,
                count: 2,
                cancellationToken: CancellationToken.None
            );

            // Assert
            totalCount.Should().Be(1); // Assert total count
            projects.Should().HaveCount(1);              // Assert paged results
            projects.First().SchoolName.Should().Be("School A");
        }

        [Fact]
        public async Task SearchForSupportProjectsWithSchoolNameLikeSchool_ShouldReturnFilteredAndPagedResults()
        {
            // Arrange
            var service = new SupportProjectRepository(fixture.Context);

            var (projects, totalCount) = await service.SearchForSupportProjects(
                title: "School",
                states: new List<string> { },
                advisors: new List<string> {  },
                regions: new List<string> { },
                localAuthorities: new List<string> { },
                page: 1,
                count: 10,
                cancellationToken: CancellationToken.None
            );

            // Assert
            totalCount.Should().Be(3); // Assert total count
            projects.Should().HaveCount(3);              // Assert paged results
            projects.First().SchoolName.Should().Be("School A");
        }

        [Fact]
        public async Task SearchForSupportProjectsWithUrn_ShouldReturnFilteredAndPagedResults()
        {
            // Arrange
            var service = new SupportProjectRepository(fixture.Context);

            var (projects, totalCount) = await service.SearchForSupportProjects(
                title: "100001",
                states: new List<string> { },
                advisors: new List<string> { },
                regions: new List<string> { },
                localAuthorities: new List<string> { },
                page: 1,
                count: 10,
                cancellationToken: CancellationToken.None
            );

            // Assert
            totalCount.Should().Be(1); // Assert total count
            projects.Should().HaveCount(1);              // Assert paged results
            projects.First().SchoolName.Should().Be("School A");
        }
    }
}

