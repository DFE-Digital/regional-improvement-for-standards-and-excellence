using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.Schools;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories
{
    public interface ISchoolRepository
    {
        Task<School?> GetPrincipalBySchoolAsync(string schoolName, CancellationToken cancellationToken);
        IQueryable<School> GetPrincipalsBySchoolsQueryable(List<string> schoolNames);

    }
}
