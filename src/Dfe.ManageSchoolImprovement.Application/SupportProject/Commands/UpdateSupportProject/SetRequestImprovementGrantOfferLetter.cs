using Dfe.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetRequestImprovementGrantOfferLetterCommand(
        SupportProjectId SupportProjectId,
        DateTime? GrantTeamContactedDate
    ) : IRequest<bool>;

    public class SetRequestImprovementGrantOfferLetterCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetRequestImprovementGrantOfferLetterCommand, bool>
    {
        public async Task<bool> Handle(SetRequestImprovementGrantOfferLetterCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetRequestImprovementGrantOfferLetter(request.GrantTeamContactedDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
