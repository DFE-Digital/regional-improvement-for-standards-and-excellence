
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Tests.Repositories
{
    public class DatabaseFixture : IDisposable
    {
        public RegionalImprovementForStandardsAndExcellenceContext Context { get; private set; }

        public DatabaseFixture()
        {
            // Configure in-memory database options
            var options = new DbContextOptionsBuilder<RegionalImprovementForStandardsAndExcellenceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Initialize the context
            Context = new RegionalImprovementForStandardsAndExcellenceContext(options, Mock.Of<IConfiguration>(), Mock.Of<IMediator>());

            // Seed data
            SeedData();
        }

        private void SeedData()
        {
            Context.SupportProjects.AddRange(
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
                "Region2",
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
            ));
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}
