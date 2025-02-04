using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;



public record SetSupportingOrganisationContactDetailsCommand(
    SupportProjectId SupportProjectId,
    string? supportingOrganisationContactName,
    string? supportingOrganisationContactEmail,
    DateTime? dateSupportOrganisationContactDetailsAdded
) : IRequest<bool>;

public class SetSupportingOrganisationContactDetails
{
    public class SetSupportingOrganisationContactDetailsHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSupportingOrganisationContactDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetSupportingOrganisationContactDetailsCommand request, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSupportingOrganisationContactDetails(request.dateSupportOrganisationContactDetailsAdded,
                request.supportingOrganisationContactName,
                request.supportingOrganisationContactEmail);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}