using Dfe.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject
{
    public record SetRecordVisitDateToVisitSchoolCommand(
        SupportProjectId SupportProjectId,
        DateTime? SchoolVisitDate
    ) : IRequest<bool>;

    public class SetRecordVisitDateToVisitSchoolCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetRecordVisitDateToVisitSchoolCommand, bool>
    {
        public async Task<bool> Handle(SetRecordVisitDateToVisitSchoolCommand request,
            CancellationToken cancellationToken)
        {
            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetSchoolVisitDate(request.SchoolVisitDate);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

            return true;
        }
    }
}
