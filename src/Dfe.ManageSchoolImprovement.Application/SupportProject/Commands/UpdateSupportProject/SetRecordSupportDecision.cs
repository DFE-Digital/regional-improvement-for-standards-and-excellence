using Dfe.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetRecordSupportDecisionCommand(
        SupportProjectId SupportProjectId,
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
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

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
