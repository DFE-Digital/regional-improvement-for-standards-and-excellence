using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using Moq;
using static Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject.SetAdviserConflictOfInterestDetails;


namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.Tests.CommandHandlers.SupportProject.UpdateSupportProject;

public class SetAdviserConflictOfInterestDetailsTests
{
    private readonly Mock<ISupportProjectRepository> _mockSupportProjectRepository;
    private readonly Domain.Entities.SupportProject.SupportProject _mockSupportProject;
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    private readonly CancellationToken _cancellationToken;
    
    public SetAdviserConflictOfInterestDetailsTests()
    { 
        
        _mockSupportProjectRepository = new Mock<ISupportProjectRepository>();
        var fixture = new Fixture();
        _mockSupportProject = fixture.Create<Domain.Entities.SupportProject.SupportProject>();
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _cancellationToken = new CancellationToken();
        _cancellationToken = CancellationToken.None;
    }

    [Fact]
    public async Task Handle_ValidCommand_UpdatesSupportProject()
    {
        // Arrange
        bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool = true;
        bool? recieveCompletedConflictOfInteresetForm = true;
        bool? saveCompletedConflictOfinterestFormInSharePoint = true;
        DateTime? dateConflictsOfInterestWereChecked = DateTime.UtcNow;

        var command = new SetAdviserConflictOfInterestDetailsCommand(
            _mockSupportProject.Id,
            sendConflictOfInterestFormToProposedAdviserAndTheSchool,
            recieveCompletedConflictOfInteresetForm,
            saveCompletedConflictOfinterestFormInSharePoint,
            dateConflictsOfInterestWereChecked
        );
        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
        var SetAdviserConflictOfInterestDetailsCommandHandler = new SetAdviserConflictOfInterestDetailsHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);
        
        // Act
        var result = await SetAdviserConflictOfInterestDetailsCommandHandler.Handle(command, _cancellationToken);
        
        // Verify
        Assert.True(result);
        _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ValidEmptyCommand_UpdatesSupportProject()
    {
        // Arrange
        // Arrange
        bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool = true;
        bool? recieveCompletedConflictOfInteresetForm = true;
        bool? saveCompletedConflictOfinterestFormInSharePoint = true;
        DateTime? dateConflictsOfInterestWereChecked = DateTime.UtcNow;

        var command = new SetAdviserConflictOfInterestDetailsCommand(
            _mockSupportProject.Id,
            null, null, null, null
        );
        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
        var SetAdviserConflictOfInterestDetailsCommandHandler = new SetAdviserConflictOfInterestDetailsHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);

        // Act
        var result = await SetAdviserConflictOfInterestDetailsCommandHandler.Handle(command, _cancellationToken);

        // Verify
        Assert.True(result);
        _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ReturnsFalse()
    {
        // Arrange
        // Arrange
        bool? sendConflictOfInterestFormToProposedAdviserAndTheSchool = true;
        bool? recieveCompletedConflictOfInteresetForm = true;
        bool? saveCompletedConflictOfinterestFormInSharePoint = true;
        DateTime? dateConflictsOfInterestWereChecked = DateTime.UtcNow;

        var command = new SetAdviserConflictOfInterestDetailsCommand(
            _mockSupportProject.Id,
            sendConflictOfInterestFormToProposedAdviserAndTheSchool,
            recieveCompletedConflictOfInteresetForm,
            saveCompletedConflictOfinterestFormInSharePoint,
            dateConflictsOfInterestWereChecked
        );

        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.SupportProject.SupportProject)null);
        var SetAdviserConflictOfInterestDetailsCommandHandler = new SetAdviserConflictOfInterestDetailsHandler(_mockSupportProjectRepository.Object, _mockDateTimeProvider.Object);

        // Act
        var result = await SetAdviserConflictOfInterestDetailsCommandHandler.Handle(command, _cancellationToken);

        // Verify
        Assert.False(result);
    }


  
}