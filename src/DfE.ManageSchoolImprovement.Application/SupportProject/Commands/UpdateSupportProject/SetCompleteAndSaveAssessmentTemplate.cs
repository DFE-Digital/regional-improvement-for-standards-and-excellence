using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetCompleteAndSaveAssessmentTemplateCommand(
        SupportProjectId SupportProjectId,
        DateTime? SavedAssessmentTemplateInSharePointDate,
        bool? HasTalkToAdviserAboutFindings,
        bool? HasCompleteAssessmentTemplate
    ) : IRequest<bool>;

    public class SetCompleteAndSaveAssessmentTemplateCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetCompleteAndSaveAssessmentTemplateCommand, bool>
    {
        public async Task<bool> Handle(SetCompleteAndSaveAssessmentTemplateCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetCompleteAndSaveAssessmentTemplate(request.SavedAssessmentTemplateInSharePointDate, request.HasTalkToAdviserAboutFindings, request.HasCompleteAssessmentTemplate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
