using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.MappingProfiles;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries;
using System;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.Tests.SupportProject.Queries
{
    public class SupportProjectQueryServiceTests
    {
        private readonly Mock<ISupportProjectRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly SupportProjectQueryService _service;

        public SupportProjectQueryServiceTests()
        {
            _repositoryMock = new Mock<ISupportProjectRepository>();
            // Configure AutoMapper with the application's mapping profile
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RiseProfile>(); // Add your application's mapping profile
            });
            _mapper = config.CreateMapper();
            _service = new SupportProjectQueryService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task SearchForSupportProjects_MapsEntitiesToDtosCorrectly()
        {
            // Arrange
            string schoolName1 = "High School A";
            string schoolUrn1 = "URN123";
            string localAuthority1 = "Local Authority A";
            string region1 = "Region A";
            string createdBy1 = "User A";
            DateTime createdOn1 = new DateTime(2023, 01, 01);

            string schoolName2 = "High School B";
            string schoolUrn2 = "URN456";
            string localAuthority2 = "Local Authority B";
            string region2 = "Region B";
            string createdBy2 = "User B";
            DateTime createdOn2 = new DateTime(2023, 02, 01);

            var projects = new List<Domain.Entities.SupportProject.SupportProject>
        {
             Domain.Entities.SupportProject.SupportProject.Create(schoolName1, schoolUrn1, localAuthority1, region1, createdBy1, createdOn1),
             Domain.Entities.SupportProject.SupportProject.Create(schoolName2, schoolUrn2, localAuthority2, region2, createdBy2, createdOn2)
        };

            _repositoryMock
                .Setup(repo => repo.SearchForSupportProjects(
                    It.IsAny<string>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((projects, projects.Count));

            // Act
            var result = await _service.SearchForSupportProjects(
                null, null, null, null, null, "/projects", 1, 10, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var response = result.Value;
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Count());

            // Verify mapping
            foreach (var dto in response.Data)
            {
                var matchingProject = projects.First(p => p.SchoolUrn == dto.schoolUrn);
                Assert.Equal(matchingProject.SchoolName, dto.schoolName);
                Assert.Equal(matchingProject.SchoolUrn, dto.schoolUrn);
                Assert.Equal(matchingProject.LocalAuthority, dto.localAuthority);
                Assert.Equal(matchingProject.Region, dto.region);
            }

            // Verify repository call
            _repositoryMock.Verify(repo => repo.SearchForSupportProjects(
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
