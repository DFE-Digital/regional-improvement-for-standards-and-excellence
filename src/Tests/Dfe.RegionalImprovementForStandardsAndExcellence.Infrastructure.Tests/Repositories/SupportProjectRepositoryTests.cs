
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Moq;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Microsoft.EntityFrameworkCore;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Dfe.RISE.Infrastructure.Tests.Repositories
{
    public class SupportProjectRepositoryTests
    {
        //[Fact]
        //public async Task SearchForSupportProjects_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var supportProjectRepository = new SupportProjectRepository(TODO);
        //    string? title = null;
        //    IEnumerable<string>? states = null;
        //    IEnumerable<string>? advisors = null;
        //    IEnumerable<string>? regions = null;
        //    IEnumerable<string>? localAuthorities = null;
        //    int page = 0;
        //    int count = 0;
        //    CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //    // Act
        //    var result = await supportProjectRepository.SearchForSupportProjects(
        //        title,
        //        states,
        //        advisors,
        //        regions,
        //        localAuthorities,
        //        page,
        //        count,
        //        cancellationToken);

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public async Task GetAllProjectRegions_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var supportProjectRepository = new SupportProjectRepository(TODO);
        //    CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //    // Act
        //    var result = await supportProjectRepository.GetAllProjectRegions(
        //        cancellationToken);

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public async Task GetAllProjectLocalAuthorities_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var supportProjectRepository = new SupportProjectRepository(TODO);
        //    CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //    // Act
        //    var result = await supportProjectRepository.GetAllProjectLocalAuthorities(
        //        cancellationToken);

        //    // Assert
        //    Assert.True(false);
        //}

        [Fact]
        public async Task SearchForSupportProjects_ShouldReturnFilteredAndPagedResults()
        {
            // Mock IConfiguration
            var mockConfiguration = new Mock<IConfiguration>();
            // Set up mock configuration behavior if needed, e.g.:
            // mockConfiguration.Setup(config => config["SomeKey"]).Returns("SomeValue");

            // Mock IMediator
            var mockMediator = new Mock<IMediator>();

            // Set up DbContextOptions with In-Memory Database
            var options = new DbContextOptionsBuilder<RegionalImprovementForStandardsAndExcellenceContext>()
                .UseInMemoryDatabase("SupportProjectsTestDb")
                .Options;

            // Create a real instance of the DbContext
            await using var context = new RegionalImprovementForStandardsAndExcellenceContext(
                options,
                mockConfiguration.Object,
                mockMediator.Object
            );

            var sampleProjects = new List<SupportProject>
        {
                 SupportProject.Create(
                "School A",
                "100001",
                "Authority1",
                "Region1",
                "User1",
                DateTime.UtcNow
            ),
            SupportProject.Create(
                "School B",
                "100002",
                "Authority2",
                "Region1",
                "User2",
                DateTime.UtcNow.AddDays(-1)
            ),
            SupportProject.Create(
                "School C",
                "100003",
                "Authority3",
                "Region2",
                "User3",
                DateTime.UtcNow.AddDays(-2)
            )
        };

            // Seed data
            context.SupportProjects.AddRange(sampleProjects);
            await context.SaveChangesAsync();

            // Act and Assert with your service
            var service = new SupportProjectRepository(context);

            var title = "Project";
            var states = new List<string> { "Active" };
            var advisors = new List<string> { "Advisor1" };
            var regions = new List<string> { "Region1" };
            var localAuthorities = new List<string> { "Authority1" };
            int page = 1;
            int count = 2;
            var cancellationToken = CancellationToken.None;

            // Act
            var (projects, totalCount) = await service.SearchForSupportProjects(
                title, states, advisors, regions, localAuthorities, page, count, cancellationToken);

            // Assert
            totalCount.Should().Be(sampleProjects.Count);
            projects.Should().HaveCount(count);
            projects.First().SchoolName.Should().Be("School A"); // Most recent project by CreatedOn
        }
    }
}

