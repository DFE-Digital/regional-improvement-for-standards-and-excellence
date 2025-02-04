using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;


public record SetChoosePreferredSupportingOrganisationCommand(
    SupportProjectId SupportProjectId,
    string? OrganisationName ,
    string? IDNumber,
    DateTime? DateSupportOrganisationChosen
) : IRequest<bool>;
public class SetChoosePreferredSupportingOrganisation
{


    public class SetChoosePreferredSupportingOrganisationHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetChoosePreferredSupportingOrganisationCommand, bool>
    {
        public async Task<bool> Handle(SetChoosePreferredSupportingOrganisationCommand request, CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetChoosePreferredSupportOrganisation(request.DateSupportOrganisationChosen,
                request.OrganisationName,
                request.IDNumber);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}