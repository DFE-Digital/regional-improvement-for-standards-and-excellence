using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Queries
{
    public interface ISupportProjectQueryService
    {
        Task<Result<IEnumerable<SupportProjectDto>>> GetAllSupportProjects(CancellationToken cancellationToken);
        Task<Result<SupportProjectDto?>> GetSupportProject(int id, CancellationToken cancellationToken);
    }
}
