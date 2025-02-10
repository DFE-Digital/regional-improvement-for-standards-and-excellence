using System.Linq.Expressions;
using AutoFixture;
using DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;
using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Moq;
using static DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject.SetContactTheSchoolDetails;


namespace DfE.ManageSchoolImprovement.Application.Tests.CommandHandlers.SupportProject.UpdateSupportProject;

public class SetContactTheSchoolDetailsTests
{
    private readonly Mock<ISupportProjectRepository> _mockSupportProjectRepository;
    private readonly Domain.Entities.SupportProject.SupportProject _mockSupportProject;
    private readonly CancellationToken _cancellationToken;
    
    public SetContactTheSchoolDetailsTests()
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
        bool? schoolEmailAddressFound =  true;
        bool? useTheNotificationLetterToCreateEmail = true; 
        bool? attachRiseInfoToEmail = true;
        DateTime? schoolContactedDate = DateTime.UtcNow;

        var command = new SetContactTheSchoolDetailsCommand(
            _mockSupportProject.Id,
            schoolEmailAddressFound,
            useTheNotificationLetterToCreateEmail,
            attachRiseInfoToEmail,
            schoolContactedDate
        );
        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
        var setContactTheSchoolDetailsCommandHandler = new SetContactTheSchoolDetailsCommandHandler(_mockSupportProjectRepository.Object);
        
        // Act
        var result = await setContactTheSchoolDetailsCommandHandler.Handle(command, _cancellationToken);
        
        // Verify
        Assert.True(result);
        _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ValidEmptyCommand_UpdatesSupportProject()
    {
        // Arrange
        var command = new SetContactTheSchoolDetailsCommand(
            _mockSupportProject.Id,
            null,
            null,
            null,
            null
        );
        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockSupportProject);
        var setContactTheSchoolDetailsCommandHandler = new SetContactTheSchoolDetailsCommandHandler(_mockSupportProjectRepository.Object);

        // Act
        var result = await setContactTheSchoolDetailsCommandHandler.Handle(command, _cancellationToken);

        // Verify
        Assert.True(result);
        _mockSupportProjectRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.SupportProject.SupportProject>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ReturnsFalse()
    {
        // Arrange
        // Arrange
        
        bool? schoolEmailAddressFound =  true;
        bool? useTheNotificationLetterToCreateEmail = true; 
        bool? attachRiseInfoToEmail = true;
        DateTime? schoolContactedDate = DateTime.UtcNow;

        var command = new SetContactTheSchoolDetailsCommand(
            _mockSupportProject.Id,
            schoolEmailAddressFound,
            useTheNotificationLetterToCreateEmail,
            attachRiseInfoToEmail,
            schoolContactedDate
        );

        _mockSupportProjectRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Domain.Entities.SupportProject.SupportProject, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.SupportProject.SupportProject)null);
        var setAdviserConflictOfInterestDetailsCommandHandler = new SetContactTheSchoolDetailsCommandHandler(_mockSupportProjectRepository.Object);

        // Act
        var result = await setAdviserConflictOfInterestDetailsCommandHandler.Handle(command, _cancellationToken);

        // Verify
        Assert.False(result);
    }


  
}
