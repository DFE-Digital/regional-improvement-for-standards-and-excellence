using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetSendAgreedImprovementPlanForApprovalCommand(
        SupportProjectId SupportProjectId,
        bool? HasSavedImprovementPlanInSharePoint, 
        bool? HasEmailedAgreedPlanToRegionalDirectorForApproval
    ) : IRequest<bool>;

    public class SetSendAgreedImprovementPlanForApprovalCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSendAgreedImprovementPlanForApprovalCommand, bool>
    {
        public async Task<bool> Handle(SetSendAgreedImprovementPlanForApprovalCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSendAgreedImprovementPlanForApproval(request.HasSavedImprovementPlanInSharePoint, request.HasEmailedAgreedPlanToRegionalDirectorForApproval);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
