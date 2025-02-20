using Dfe.ManageSchoolImprovement.Domain.Entities.SupportProject;
using Dfe.ManageSchoolImprovement.Infrastructure.Database;
using Dfe.ManageSchoolImprovement.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Dfe.ManageSchoolImprovement.Infrastructure.Tests.Repositories
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

            var mockUserService = Mock.Of<IUserContextService>();
            Mock.Get(mockUserService).Setup(x => x.GetCurrentUsername()).Returns("test@user.com");

            // Initialize the context
            Context = new RegionalImprovementForStandardsAndExcellenceContext(options, Mock.Of<IConfiguration>(), Mock.Of<IMediator>(), mockUserService);

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
                "Region1"
            ),
            SupportProject.Create(
                "School B",
                "100002",
                "Authority2",
                "Region2"
            ),
            SupportProject.Create(
                "School C",
                "100003",
                "Authority3",
                "Region2"
            ),
            SupportProject.Create(
                "School D",
                "100004",
                "Authority5",
                "Region3",
                DateTime.Now
            ));
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}
