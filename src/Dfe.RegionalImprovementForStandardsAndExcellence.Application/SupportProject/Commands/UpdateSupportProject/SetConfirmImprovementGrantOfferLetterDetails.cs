using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public class SetConfirmImprovementGrantOfferLetterDetails
{
    public record SetConfirmImprovementGrantOfferLetterDetailsCommand(
        SupportProjectId SupportProjectId,
        DateTime? DateImprovementGrantOfferLetterSent
    ) : IRequest<bool>;

    public class SetConfirmImprovementGrantOfferLetterDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetConfirmImprovementGrantOfferLetterDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetConfirmImprovementGrantOfferLetterDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetConfirmImprovementGrantOfferLetterDetails(request.DateImprovementGrantOfferLetterSent);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}