using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public class SetNoteOfVisitDetails
{
    public record SetNoteOfVisitDetailsCommand(SupportProjectId SupportProjectId,
                                               bool? GiveTheAdviserTheNoteOfVisitTemplate,
                                               bool? AskTheAdviserToSendYouTheirNotes,
                                               DateTime? DateNoteOfVisitSavedInSharePoint) : IRequest<bool>;

    public class SetNoteOfVisitDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetNoteOfVisitDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetNoteOfVisitDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetNoteOfVisitDetails(request.GiveTheAdviserTheNoteOfVisitTemplate,
                                                               request.AskTheAdviserToSendYouTheirNotes,
                                                               request.DateNoteOfVisitSavedInSharePoint);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
