using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
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
        SupportProjectNoteId Id
    ) : IRequest<SupportProjectNoteId>;
    
    public class EditSupportProjectNoteCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<EditSupportProjectNoteCommand, SupportProjectNoteId>
    {
        public async Task<SupportProjectNoteId> Handle(EditSupportProjectNoteCommand request, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.GetSupportProjectById(request.SupportProjectId, cancellationToken);

            supportProject.Notes.FirstOrDefault(a => a.Id == request.Id).Note = request.Note;
            
            await supportProjectRepository.UpdateAsync(supportProject);

            return request.Id;
        }
    }
}