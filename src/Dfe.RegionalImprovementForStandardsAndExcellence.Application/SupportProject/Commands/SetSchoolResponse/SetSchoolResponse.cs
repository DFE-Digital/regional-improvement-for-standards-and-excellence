using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.SetSchoolResponse
{
    public record SetSchoolResponseCommand(
        SupportProjectId id,
        DateTime? SchoolResponseDate,
        bool? HasAcceeptedTargetedSupport,
        bool? HasSavedSchoolResponseinSharePoint
    ) : IRequest<bool>;

    public class SetSchoolResponseCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSchoolResponseCommand, bool>
    {
        public async Task<bool> Handle(SetSchoolResponseCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSchoolResponse(request.SchoolResponseDate, request.HasAcceeptedTargetedSupport, request.HasSavedSchoolResponseinSharePoint);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
