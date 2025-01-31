using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetRecordSupportingOrganisationAppointmentCommand(
        SupportProjectId SupportProjectId,
        DateTime? RegionalDirectorAppointmentDate,
        bool? HasConfirmedSupportingOrgnaisationAppointment,
        string? DisapprovingSupportingOrgnaisationAppointmentNotes
    ) : IRequest<bool>;

    public class SetRecordSupportingOrganisationAppointmentCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetRecordSupportingOrganisationAppointmentCommand, bool>
    {
        public async Task<bool> Handle(SetRecordSupportingOrganisationAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetRecordSupportingOrganisationAppointment(request.RegionalDirectorAppointmentDate, request.HasConfirmedSupportingOrgnaisationAppointment, request.DisapprovingSupportingOrgnaisationAppointmentNotes);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}