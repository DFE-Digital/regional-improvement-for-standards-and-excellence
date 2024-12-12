using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;
using System.Diagnostics.CodeAnalysis;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class SupportProjectRepository(RegionalImprovementForStandardsAndExcellenceContext dbContext)
        : Repository<SupportProject, RegionalImprovementForStandardsAndExcellenceContext>(dbContext), ISupportProjectRepository
    {
        
    }
}
