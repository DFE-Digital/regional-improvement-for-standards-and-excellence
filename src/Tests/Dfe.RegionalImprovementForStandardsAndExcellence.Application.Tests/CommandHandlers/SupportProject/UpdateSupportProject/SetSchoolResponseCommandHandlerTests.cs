using AutoFixture;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Moq;
using System.Linq.Expressions;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.SetSchoolResponse;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.Tests.CommandHandlers.SupportProject.UpdateSupportProject
{
    public class SetSchoolResponseCommandHandlerTests
    {
        private readonly Mock<ISupportProjectRepository> _mockSupportProjectRepository;
        private readonly Domain.Entities.SupportProject.SupportProject _mockSupportProject;
        private readonly CancellationToken _cancellationToken;

        public SetSchoolResponseCommandHandlerTests()
        {

            _mockSupportProjectRepository = new Mock<ISupportProjectRepository>();
            var fixture = new Fixture();
            _mockSupportProject = fixture.Create<Domain.Entities.SupportProject.SupportProject>();
            _cancellationToken = new CancellationToken();
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesSupportProject()
        {
            // Arrange
            var schoolResponseDate = DateTime.UtcNow;
            var hasAcceeptedTargetedSupport = true;
            var hasSavedSchoolResponseinSharePoint = true;

            var command = new SetSchoolResponseCommand(
                _mockSupportProject.Id,
                schoolResponseDate,
                hasAcceeptedTargetedSupport,
                hasSavedSchoolResponseinSharePoint
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var handler = new SetSchoolResponseCommandHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await handler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidEmptyCommand_UpdatesSupportProject()
        {
            // Arrange
            var command = new SetSchoolResponseCommand(
                _mockSupportProject.Id,
                null,
                null,
                null
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var handler = new SetSchoolResponseCommandHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await handler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProjectNotFound_ReturnsFalse()
        {
            // Arrange
            var schoolResponseDate = DateTime.UtcNow;
            var hasAcceeptedTargetedSupport = true;
            var hasSavedSchoolResponseinSharePoint = true;

            var command = new SetSchoolResponseCommand(
                _mockSupportProject.Id,
                schoolResponseDate,
                hasAcceeptedTargetedSupport,
                hasSavedSchoolResponseinSharePoint
            );

            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.SupportProject.SupportProject)null);
            var handler = new SetSchoolResponseCommandHandler(_mockSupportProjectRepository.Object);

            // Act
            var result = await handler.Handle(command, _cancellationToken);

            // Verify
            Assert.False(result);
        }
    }
}
