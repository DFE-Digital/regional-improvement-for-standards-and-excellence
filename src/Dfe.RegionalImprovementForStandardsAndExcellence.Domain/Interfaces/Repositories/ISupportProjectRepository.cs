using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories
{
    public interface ISupportProjectRepository : IRepository<SupportProject>
    {
        Task<(IEnumerable<SupportProject> projects, int totalCount)> SearchForSupportProjects(
            string? title,
            IEnumerable<string>? states,
            IEnumerable<string>? advisors,
            IEnumerable<string>? regions,
            IEnumerable<string>? localAuthorities,
            int page,
            int count, 
            CancellationToken cancellationToken);

        Task<IEnumerable<string>> GetAllProjectRegions(CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetAllProjectLocalAuthorities(CancellationToken cancellationToken);
        
        Task<SupportProject> GetSupportProjectById(SupportProjectId id, CancellationToken cancellationToken);
    }
}
