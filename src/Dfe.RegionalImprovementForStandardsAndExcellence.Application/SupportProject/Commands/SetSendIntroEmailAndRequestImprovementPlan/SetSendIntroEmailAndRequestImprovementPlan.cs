using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.SetSendIntroEmailAndRequestImprovementPlan
{
    public record SetSendIntroEmailAndRequestImprovementPlanCommand(
        SupportProjectId id,
        DateTime? IntroductoryEmailSentDate,
        bool? HasShareEmailTemplateWithAdvisor,
        bool? RemindAdvisorToCopyRiseTeamWhenSentEmail
    ) : IRequest<bool>;

    public class SetSendIntroEmailAndRequestImprovementPlanCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSendIntroEmailAndRequestImprovementPlanCommand, bool>
    {
        public async Task<bool> Handle(SetSendIntroEmailAndRequestImprovementPlanCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSentroductEmailAndRequestImprovementPlan(request.IntroductoryEmailSentDate, request.HasShareEmailTemplateWithAdvisor, request.RemindAdvisorToCopyRiseTeamWhenSentEmail);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
