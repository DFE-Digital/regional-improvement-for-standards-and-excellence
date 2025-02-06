using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject
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
