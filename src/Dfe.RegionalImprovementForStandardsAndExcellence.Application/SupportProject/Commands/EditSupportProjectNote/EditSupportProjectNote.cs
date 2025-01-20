using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.EditSupportProjectNote;

public class EditSupportProjectNote
{
    public record EditSupportProjectNoteCommand(
        SupportProjectId SupportProjectId,
        string Note,
        SupportProjectNoteId Id,
        string Author
    ) : IRequest<SupportProjectNoteId>;
    
    public class EditSupportProjectNoteCommandHandler(ISupportProjectRepository supportProjectRepository,IDateTimeProvider _dateTimeProvider)
        : IRequestHandler<EditSupportProjectNoteCommand, SupportProjectNoteId>
    {
        public async Task<SupportProjectNoteId> Handle(EditSupportProjectNoteCommand request, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.GetSupportProjectById(request.SupportProjectId, cancellationToken);

            supportProject.EditSupportProjectNote(request.Id,request.Note,request.Author,_dateTimeProvider.Now);
            
            await supportProjectRepository.UpdateAsync(supportProject);
            
            return request.Id;
        }
    }
}