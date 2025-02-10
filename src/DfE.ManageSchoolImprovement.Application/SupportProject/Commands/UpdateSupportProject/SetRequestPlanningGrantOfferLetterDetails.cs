using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public class SetRequestPlanningGrantOfferLetterDetails
{
    public record SetRequestPlanningGrantOfferLetterDetailsCommand(
        SupportProjectId SupportProjectId,
        DateTime? DateGrantTeamContacted
    ) : IRequest<bool>;

    public class SetRequestPlanningGrantOfferLetterDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetRequestPlanningGrantOfferLetterDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetRequestPlanningGrantOfferLetterDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetRequestPlanningGrantOfferLetterDetails(request.DateGrantTeamContacted);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}
