using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using DfE.ManageSchoolImprovement.Utils;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.EditSupportProjectNote;

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
            
            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);
            
            return request.Id;
        }
    }
}
