//using System.Diagnostics.CodeAnalysis;
//using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common;
//using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
//using Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Database;

//namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Repositories
//{
//    [ExcludeFromCodeCoverage]
//    public class SclRepository<TAggregate>(RegionalImprovementForStandardsAndExcellenceContext dbContext)
//        : Repository<TAggregate, RegionalImprovementForStandardsAndExcellenceContext>(dbContext), ISclRepository<TAggregate>
//        where TAggregate : class, IAggregateRoot
//    {
//    }
//}