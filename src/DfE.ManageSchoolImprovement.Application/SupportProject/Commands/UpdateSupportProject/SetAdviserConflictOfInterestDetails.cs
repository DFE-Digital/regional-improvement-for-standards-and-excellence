using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public class SetAdviserConflictOfInterestDetails
{
    public record SetAdviserConflictOfInterestDetailsCommand(
        SupportProjectId SupportProjectId,
        bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool,
        bool? ReceiveCompletedConflictOfInterestForm,
        bool? SaveCompletedConflictOfinterestFormInSharePoint,
        DateTime? DateConflictsOfInterestWereChecked
    ) : IRequest<bool>;

    public class SetAdviserConflictOfInterestDetailsHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetAdviserConflictOfInterestDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetAdviserConflictOfInterestDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetAdviserConflictOfInterestDetails(request.SendConflictOfInterestFormToProposedAdviserAndTheSchool,
                                                               request.ReceiveCompletedConflictOfInterestForm,
                                                               request.SaveCompletedConflictOfinterestFormInSharePoint,
                                                               request.DateConflictsOfInterestWereChecked);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
