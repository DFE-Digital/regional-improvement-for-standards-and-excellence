using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public record SetConfirmPlanningGrantOfferLetterSentCommand(
    SupportProjectId SupportProjectId,
    DateTime? ConfirmPlanningGrantOfferLetterSentDate
) : IRequest<bool>;

public class SetConfirmPlanningGrantOfferLetterSentCommandHandler(ISupportProjectRepository supportProjectRepository)
    : IRequestHandler<SetConfirmPlanningGrantOfferLetterSentCommand, bool>
{
    public async Task<bool> Handle(SetConfirmPlanningGrantOfferLetterSentCommand request,
        CancellationToken cancellationToken)
    {
        var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

        if (supportProject is null)
        {
            return false;
        }

        supportProject.SetConfirmPlanningGrantOfferLetterDate(request.ConfirmPlanningGrantOfferLetterSentDate);

        await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

        return true;
    }
}
