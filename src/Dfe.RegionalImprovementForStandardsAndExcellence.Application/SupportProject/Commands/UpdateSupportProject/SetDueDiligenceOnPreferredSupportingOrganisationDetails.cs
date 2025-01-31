using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using Dfe.RegionalImprovementForStandardsAndExcellence.Utils;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public class SetDueDiligenceOnPreferredSupportingOrganisationDetails
{
    public record SetDueDiligenceOnPreferredSupportingOrganisationDetailsCommand(
        SupportProjectId SupportProjectId,
         bool? CheckOrganisationHasCapacityAndWillingToProvideSupport,
         bool? CheckChoiceWithTrustRelationshipManagerOrLaLead,
         bool? DiscussChoiceWithSfso,
         bool? CheckFinancialConcernsAtSupportingOrganisation,
         bool? CheckTheOrganisationHasAVendorAccount,
         DateTime? DateDueDiligenceCompleted
    ) : IRequest<bool>;

    public class SetDueDiligenceOnPreferredSupportingOrganisationDetailsCommandHandler(ISupportProjectRepository supportProjectRepository, IDateTimeProvider _dateTimeProvider)
        : IRequestHandler<SetDueDiligenceOnPreferredSupportingOrganisationDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetDueDiligenceOnPreferredSupportingOrganisationDetailsCommand request, CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetDueDiligenceOnPreferredSupportingOrganisationDetails(
                request.CheckOrganisationHasCapacityAndWillingToProvideSupport,
                request.CheckChoiceWithTrustRelationshipManagerOrLaLead,
                request.DiscussChoiceWithSfso,
                request.CheckFinancialConcernsAtSupportingOrganisation,
                request.CheckTheOrganisationHasAVendorAccount,
                request.DateDueDiligenceCompleted);

            await supportProjectRepository.UpdateAsync(supportProject);

            return true;
        }
    }
}