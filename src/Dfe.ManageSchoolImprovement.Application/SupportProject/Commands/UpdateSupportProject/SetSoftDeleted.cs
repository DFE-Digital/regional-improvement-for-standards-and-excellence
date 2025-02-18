using Dfe.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;

public record SetSoftDeletedCommand(
    SupportProjectId SupportProjectId
) : IRequest<bool>;


public class SetSoftDeleted
{
    public class SetSoftDeletedCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetSoftDeletedCommand, bool>    {

        public async Task<bool> Handle(SetSoftDeletedCommand request,
            CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSoftDeleted();

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);
            
            return true;
        }
    }
}
