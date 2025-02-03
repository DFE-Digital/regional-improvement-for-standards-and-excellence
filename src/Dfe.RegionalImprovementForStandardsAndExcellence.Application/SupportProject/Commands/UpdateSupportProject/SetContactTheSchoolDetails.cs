using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public record SetContactTheSchoolDetailsCommand(
    SupportProjectId SupportProjectId,
    bool? FindSchoolEmailAddress,
    bool? UseTheNotificationLetterToCreateEmail,
    bool? AttachRiseInfoToEmail,
    DateTime? SchoolContactedDate
) : IRequest<bool>;

public class SetContactTheSchoolDetails
{
    public class SetContactTheSchoolDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetContactTheSchoolDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetContactTheSchoolDetailsCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }
            
            supportProject.SetContactTheSchoolDetails(request.FindSchoolEmailAddress, request.UseTheNotificationLetterToCreateEmail,request.AttachRiseInfoToEmail, request.SchoolContactedDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}