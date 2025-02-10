using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetSendIntroductoryEmailCommand(
        SupportProjectId SupportProjectId,
        DateTime? IntroductoryEmailSentDate,
        bool? HasShareEmailTemplateWithAdvisor,
        bool? RemindAdvisorToCopyRiseTeamWhenSentEmail
    ) : IRequest<bool>;

    public class SetSendIntroductoryEmailCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSendIntroductoryEmailCommand, bool>
    {
        public async Task<bool> Handle(SetSendIntroductoryEmailCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSendIntroductoryEmail(request.IntroductoryEmailSentDate, request.HasShareEmailTemplateWithAdvisor, request.RemindAdvisorToCopyRiseTeamWhenSentEmail);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
