using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public class SetImprovementPlanTemplateDetails
{
    public record SetImprovementPlanTemplateDetailsCommand(
        SupportProjectId SupportProjectId,
        bool? SendTheTemplateToTheSupportingOrganisation,
        bool? SendTheTemplateToTheSchoolsResponsibleBody,
        DateTime? DateTemplatesSent
    ) : IRequest<bool>;

    public class SetImprovementPlanTemplateDetailsHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetImprovementPlanTemplateDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetImprovementPlanTemplateDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetImprovementPlanTemplateDetails(request.SendTheTemplateToTheSupportingOrganisation,
                                                               request.SendTheTemplateToTheSchoolsResponsibleBody,
                                                               request.DateTemplatesSent);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}
