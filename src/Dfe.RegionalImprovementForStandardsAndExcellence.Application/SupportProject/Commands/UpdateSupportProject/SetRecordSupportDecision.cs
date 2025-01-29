using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetRecordSupportDecisionCommand(
        SupportProjectId id,
        DateTime? RegionalDirectorDecisionDate,
        bool? HasConfirmedSchoolGetTargetSupport,
        string? DisapprovingTargetedSupportNotes
    ) : IRequest<bool>;

    public class SetRecordSupportDecisionCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetRecordSupportDecisionCommand, bool>
    {
        public async Task<bool> Handle(SetRecordSupportDecisionCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetRecordSupportDecision(request.RegionalDirectorDecisionDate, request.HasConfirmedSchoolGetTargetSupport, request.DisapprovingTargetedSupportNotes);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
