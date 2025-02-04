using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public class SetAdviserDetails
{
    public record SetAdviserDetailsCommand(
        SupportProjectId SupportProjectId,
        DateTime? DateAdviserAssigned,
        string? AdviserEmailAddress
    ) : IRequest<bool>;

    public class SetAdviserDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetAdviserDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetAdviserDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetAdviserDetails(request.AdviserEmailAddress, request.DateAdviserAssigned);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}