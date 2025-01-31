using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.CreateSupportProject
{
    public record CreateSupportProjectCommand(
        string schoolName,
        string schoolUrn,
        string localAuthority,
        string region
    ) : IRequest<SupportProjectId>;

    public class CreateSupportProjectCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<CreateSupportProjectCommand, SupportProjectId>
    {
        public async Task<SupportProjectId> Handle(CreateSupportProjectCommand request, CancellationToken cancellationToken)
        {
            var supportProject = Domain.Entities.SupportProject.SupportProject.Create(request.schoolName, request.schoolUrn, request.localAuthority,request.region);

            await supportProjectRepository.AddAsync(supportProject, cancellationToken);

            return supportProject.Id!;
        }
    }
}
