using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories
{
    internal class SupportProjectRepository(SclContext dbContext)
        : Repository<SupportProject, SclContext>(dbContext), ISupportProjectRepository
    {
    }
}
