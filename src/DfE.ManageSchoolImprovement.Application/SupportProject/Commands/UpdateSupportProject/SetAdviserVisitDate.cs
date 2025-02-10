using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public record SetAdviserVisitDateCommand(
    SupportProjectId SupportProjectId,
    DateTime? AdviserVisitDate
) : IRequest<bool>;

public class SetAdviserVisitDate
{
    public class SetAdviserVisitDateCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetAdviserVisitDateCommand, bool>
    {
        public async Task<bool> Handle(SetAdviserVisitDateCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }
            
            supportProject.SetAdviserVisitDate(request.AdviserVisitDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
