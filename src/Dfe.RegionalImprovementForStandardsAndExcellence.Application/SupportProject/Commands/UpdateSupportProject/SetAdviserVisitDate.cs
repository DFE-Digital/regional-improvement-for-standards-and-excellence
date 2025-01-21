using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public record SetAdviserVisitDateCommand(
    SupportProjectId id,
    DateTime? adviserVisitDate
) : IRequest<bool>;

public class SetAdviserVisitDate
{
    public class SetAdviserVisitDateCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetAdviserVisitDateCommand, bool>
    {
        public async Task<bool> Handle(SetAdviserVisitDateCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }
            
            supportProject.SetAdviserVisitDate(request.adviserVisitDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}