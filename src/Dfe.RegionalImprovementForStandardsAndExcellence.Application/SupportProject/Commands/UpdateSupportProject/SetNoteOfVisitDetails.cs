using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public class SetNoteOfVisitDetails
{
    public record SetNoteOfVisitDetailsCommand(SupportProjectId SupportProjectId,
                                               bool? giveTheAdviserTheNoteOfVisitTemplate,
                                               bool? askTheAdviserToSendYouTheirNotes,
                                               DateTime? dateNoteOfVisitSavedInSharePoint) : IRequest<bool>;

    public class SetNoteOfVisitDetailsCommandHandler(ISupportProjectRepository supportProjectRepository, IDateTimeProvider _dateTimeProvider)
        : IRequestHandler<SetNoteOfVisitDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetNoteOfVisitDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetNoteOfVisitDetails(request.giveTheAdviserTheNoteOfVisitTemplate,
                                                               request.askTheAdviserToSendYouTheirNotes,
                                                               request.dateNoteOfVisitSavedInSharePoint);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}