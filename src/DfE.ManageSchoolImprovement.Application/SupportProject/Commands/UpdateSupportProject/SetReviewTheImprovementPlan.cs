using DfE.ManageSchoolImprovement.Domain.Interfaces.Repositories;
using DfE.ManageSchoolImprovement.Domain.ValueObjects;
using MediatR;

namespace DfE.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;


public record SetReviewTheImprovementPlanCommand(
    SupportProjectId SupportProjectId,
    DateTime? ImprovementPlanReceivedDate,
    bool? ReviewImprovementPlanWithTeam
) : IRequest<bool>;

public class SetReviewTheImprovementPlanCommandHandler(ISupportProjectRepository supportProjectRepository)
    : IRequestHandler<SetReviewTheImprovementPlanCommand, bool>
{
    public async Task<bool> Handle(SetReviewTheImprovementPlanCommand request,
        CancellationToken cancellationToken)
    {
        var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.SupportProjectId, cancellationToken);

        if (supportProject is null)
        {
            return false;
        }

        supportProject.SetReviewTheImprovementPlan(request.ImprovementPlanReceivedDate,request.ReviewImprovementPlanWithTeam);

        await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);

        return true;
    }
}
