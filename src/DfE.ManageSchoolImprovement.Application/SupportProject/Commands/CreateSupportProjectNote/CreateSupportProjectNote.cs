using DfE.ManageSchoolImprovement.Application.SupportProject.Commands.CreateSupportProject;
using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using DfE.ManageSchoolImprovement.Utils;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.CreateSupportProjectNote;

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

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return supportProjectNoteId;
        }
    }
}
