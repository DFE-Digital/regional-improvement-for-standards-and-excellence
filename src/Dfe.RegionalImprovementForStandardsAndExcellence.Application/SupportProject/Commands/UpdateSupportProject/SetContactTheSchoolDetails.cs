using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public record SetContactTheSchoolDetailsCommand(
    SupportProjectId id,
    bool? findSchoolEmailAddress,
    bool? useTheNotificationLetterToCreateEmail,
    bool? attachRiseInfoToEmail,
    DateTime? schoolContactedDate
) : IRequest<bool>;

public class SetContactTheSchoolDetails
{
    public class SetContactTheSchoolDetailsCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetContactTheSchoolDetailsCommand, bool>
    {
        public async Task<bool> Handle(SetContactTheSchoolDetailsCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }
            
            supportProject.SetContactTheSchoolDetails(request.findSchoolEmailAddress, request.useTheNotificationLetterToCreateEmail,request.attachRiseInfoToEmail,request.schoolContactedDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}