using AutoFixture;
using Dfe.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Moq;
using System.Linq.Expressions;
using static Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject.SetImprovementPlanTemplateDetails;

namespace Dfe.ManageSchoolImprovement.Application.Tests.CommandHandlers.SupportProject.UpdateSupportProject
{
    public class SetShareImprovementPlanDetailsTests
    {
        private readonly Mock<ISupportProjectRepository> _mockSupportProjectRepository;
        private readonly Domain.Entities.SupportProject.SupportProject _mockSupportProject;
        private readonly CancellationToken _cancellationToken;

        public SetShareImprovementPlanDetailsTests()
        {

            _mockSupportProjectRepository = new Mock<ISupportProjectRepository>();
            var fixture = new Fixture();
            _mockSupportProject = fixture.Create<Domain.Entities.SupportProject.SupportProject>();
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesSupportProject()
        {
            // Arrange
            DateTime? dateTemplatesSent = DateTime.UtcNow;
            bool? sendTheTemplateToTheSupportingOrganisation = true;
            bool? sendTheTemplateToTheSchoolsResponsibleBody = true;

            var command = new SetImprovementPlanTemplateDetailsCommand(
                _mockSupportProject.Id,
                sendTheTemplateToTheSupportingOrganisation,
                sendTheTemplateToTheSchoolsResponsibleBody,
                dateTemplatesSent
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var SetImprovementPlanTemplateDetailsCommandHandler = new SetImprovementPlanTemplateDetailsHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await SetImprovementPlanTemplateDetailsCommandHandler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidEmptyCommand_UpdatesSupportProject()
        {
            // Arrange
            var command = new SetImprovementPlanTemplateDetailsCommand(
               _mockSupportProject.Id,
               null,
               null,
               null
           );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var SetImprovementPlanTemplateDetailsCommandHandler = new SetImprovementPlanTemplateDetailsHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await SetImprovementPlanTemplateDetailsCommandHandler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProjectNotFound_ReturnsFalse()
        {
            // Arrange
            DateTime? dateTemplatesSent = DateTime.UtcNow;
            bool? sendTheTemplateToTheSupportingOrganisation = true;
            bool? sendTheTemplateToTheSchoolsResponsibleBody = true;

            var command = new SetImprovementPlanTemplateDetailsCommand(
                _mockSupportProject.Id,
                sendTheTemplateToTheSupportingOrganisation,
                sendTheTemplateToTheSchoolsResponsibleBody,
                dateTemplatesSent
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.SupportProject.SupportProject)null);
            var SetImprovementPlanTemplateDetailsCommandHandler = new SetImprovementPlanTemplateDetailsHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await SetImprovementPlanTemplateDetailsCommandHandler.Handle(command, _cancellationToken);
            // Verify
            Assert.False(result);
        }
    }
}
