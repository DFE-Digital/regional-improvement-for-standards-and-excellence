using MediatR;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
