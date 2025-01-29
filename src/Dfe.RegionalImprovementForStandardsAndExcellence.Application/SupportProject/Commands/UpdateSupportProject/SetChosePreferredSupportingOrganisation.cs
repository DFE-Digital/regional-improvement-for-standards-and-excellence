using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;


public record SetChosePreferredSupportingOrganisationCommand(
    SupportProjectId SupportProjectId,
    string? organisationName ,
    string? iDNumber,
    DateTime? dateSupportOrganisationChosen
) : IRequest<bool>;
public class SetChosePreferredSupportingOrganisation
{


    public class SetChosePreferredSupportingOrganisationHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetChosePreferredSupportingOrganisationCommand, bool>
    {
        public async Task<bool> Handle(SetChosePreferredSupportingOrganisationCommand request, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetChoosePreferredSupportOrganisation(request.dateSupportOrganisationChosen,
                request.organisationName,
                request.iDNumber);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}