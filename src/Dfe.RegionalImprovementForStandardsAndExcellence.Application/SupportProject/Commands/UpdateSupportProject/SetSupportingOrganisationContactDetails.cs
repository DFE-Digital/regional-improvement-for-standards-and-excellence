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
    
}