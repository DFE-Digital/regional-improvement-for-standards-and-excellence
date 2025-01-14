using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProjectNote;

public class CreateSupportProjectNote
{
    public record CreateSupportProjectNoteCommand(
        SupportProjectId SupportProjectId,
        string Note,
        string Author
    ) : IRequest<SupportProjectNoteId>;
    
    public class CreateSupportProjectNoteCommandHandler(ISupportProjectRepository supportProjectRepository, IDateTimeProvider _dateTimeProvider)
        : IRequestHandler<CreateSupportProjectNoteCommand, SupportProjectNoteId>
    {
        public async Task<SupportProjectNoteId> Handle(CreateSupportProjectNoteCommand request, CancellationToken cancellationToken)
        {
            
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);
           
            var supportProjectNoteId = new SupportProjectNoteId(Guid.NewGuid());
            
            supportProject.AddNote(supportProjectNoteId,request.Note,request.Author,_dateTimeProvider.Now,request.SupportProjectId);

            await supportProjectRepository.UpdateAsync(supportProject);

            return supportProjectNoteId;
        }
    }
}