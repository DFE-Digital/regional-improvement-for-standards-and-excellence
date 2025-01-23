﻿using AutoFixture;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject.SetNoteOfVisitDetails;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.Tests.CommandHandlers.SupportProject.UpdateSupportProject
{
    public class SetNoteOfVisitDetailsTests
    {
        private readonly Mock<ISupportProjectRepository> _mockSupportProjectRepository;
        private readonly Domain.Entities.SupportProject.SupportProject _mockSupportProject;
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
        private readonly CancellationToken _cancellationToken;

        public SetNoteOfVisitDetailsTests()
        {

            _mockSupportProjectRepository = new Mock<ISupportProjectRepository>();
            var fixture = new Fixture();
            _mockSupportProject = fixture.Create<Domain.Entities.SupportProject.SupportProject>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesSupportProject()
        {
            // Arrange
            bool? giveTheAdviserTheNoteOfVisitTemplate = false;
            bool? askTheAdviserToSendYouTheirNotes = false;
            DateTime? dateNoteOfVisitSavedInSharePoint = DateTime.UtcNow;

            var command = new SetNoteOfVisitDetailsCommand(
                _mockSupportProject.Id,
                giveTheAdviserTheNoteOfVisitTemplate,
                askTheAdviserToSendYouTheirNotes,
                dateNoteOfVisitSavedInSharePoint
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var SetNoteOfVisitDetailsCommandHandler = new SetNoteOfVisitDetailsCommandHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);

            // Act
            var result = await SetNoteOfVisitDetailsCommandHandler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidEmptyCommand_UpdatesSupportProject()
        {
            // Arrange
            var command = new SetNoteOfVisitDetailsCommand(
                _mockSupportProject.Id,
                null,
                null,
                null
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
            var SetNoteOfVisitDetailsCommandHandler = new SetNoteOfVisitDetailsCommandHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);

            // Act
            var result = await SetNoteOfVisitDetailsCommandHandler.Handle(command, _cancellationToken);

            // Verify
            Assert.True(result);
            _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProjectNotFound_ReturnsFalse()
        {
            // Arrange
            bool? giveTheAdviserTheNoteOfVisitTemplate = false;
            bool? askTheAdviserToSendYouTheirNotes = false;
            DateTime? dateNoteOfVisitSavedInSharePoint = DateTime.UtcNow;

            var command = new SetNoteOfVisitDetailsCommand(
                _mockSupportProject.Id,
                giveTheAdviserTheNoteOfVisitTemplate,
                askTheAdviserToSendYouTheirNotes,
                dateNoteOfVisitSavedInSharePoint
            );
            _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.SupportProject.SupportProject)null);
            var SetNoteOfVisitDetailsCommandHandler = new SetNoteOfVisitDetailsCommandHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);

            // Act
            var result = await SetNoteOfVisitDetailsCommandHandler.Handle(command, _cancellationToken);

            // Verify
            Assert.False(result);
        }
    }
}
