using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public class SetAdviserConflictOfInterestDetails
{
    public record SetAdviserConflictOfInterestDetailsCommand(
        SupportProjectId SupportProjectId,
        bool? SendConflictOfInterestFormToProposedAdviserAndTheSchool,
        bool? RecieveCompletedConflictOfInteresetForm,
        bool? SaveCompletedConflictOfinterestFormInSharePoint,
        DateTime? DateConflictsOfInterestWereChecked
    ) : IRequest<bool>;

    public class SetAdviserConflictOfInterestDetailsHandler(ISupportProjectRepository supportProjectRepository, IDateTimeProvider _dateTimeProvider)
        : IRequestHandler<SetAdviserConflictOfInterestDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetAdviserConflictOfInterestDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            var supportProjectNoteId = new SupportProjectNoteId(Guid.NewGuid());

            supportProject.SetAdviserConflictOfInterestDetails(request.SendConflictOfInterestFormToProposedAdviserAndTheSchool,
                                                               request.RecieveCompletedConflictOfInteresetForm,
                                                               request.SaveCompletedConflictOfinterestFormInSharePoint,
                                                               request.DateConflictsOfInterestWereChecked);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}